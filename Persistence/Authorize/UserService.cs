using Application.ExceptionTypes;
using Application.JwtTokenHandlerInterface.AuthenticationsInterfaces;
using Application.JwtTokenHandlerInterface.DTOs;
using Application.Repositories.EndpointRepositories;
using Domain.BaseProjeEntities.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Persistence.Authorize;

public class UserService(UserManager<AppUser> userManager,IEndpointReadRepository endpointReadRepository) : IUserService
{
    public async Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model)
    {


        IdentityResult result = await userManager.CreateAsync(new()
        {
            Id = Guid.NewGuid(),
            UserName = model.Username,
            Email = model.Email,
            NameSurname = model.NameSurname,
        }, model.Password);


        CreateUserResponseDTO response = new() { Succeeded = result.Succeeded };


        if (result.Succeeded)
        {
            response.Message = "Kullanıcı başarıyla Oluşturulmuştur";
        }
        else
        {
            foreach (var error in result.Errors)
            {
                response.Message += $"{error.Code} - {error.Description}\n";
            }
        }
        return response;
    }

    public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
    {

        if (user is not null)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddMinutes(addOnAccessTokenDate);
            await userManager.UpdateAsync(user);
        }
        else
        {
            throw new NotFoundException();
        }

    }
    public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
    {
        AppUser? user = await userManager.FindByIdAsync(userId);
        if(user is not null)
        {
            byte[] tokenBytes = WebEncoders.Base64UrlDecode(resetToken);
            resetToken = Encoding.UTF8.GetString(tokenBytes);
          IdentityResult result = await userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if(result.Succeeded)
            {
               await userManager.UpdateSecurityStampAsync(user);
            }
            else
            {
                throw new OperationException("Şifre güncelleme işlemi başarısız");
            }
        }
    }

    public async Task<List<ListUserDto>> GetAllUsersAsync()
    {
       var users = await userManager.Users.ToListAsync();

        return users.Select(user => new ListUserDto
        {
          Id = user.Id,
          Email = user.Email,
          NameSurname = user.NameSurname,
          TwoFactorEnabled = user.TwoFactorEnabled,
          UserName = user.UserName
        }).ToList();
    }

    public async Task AssignRoleToUserAsync(string userId, string[] roles)
    {
       AppUser user = await userManager.FindByIdAsync(userId);
        if(user is not null )
        {
           var userRoles =await userManager.GetRolesAsync(user);
            await userManager.RemoveFromRolesAsync(user, userRoles);

            await userManager.AddToRolesAsync(user, roles);
        }
    }

    public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
    {
        AppUser user = await userManager.FindByNameAsync(userIdOrName);

        
        if(user is not null )
        {
            var userRoles = await userManager.GetRolesAsync(user);
           return userRoles.ToArray();
        }
        else
        {
            throw new NotFoundException("User Bulunamadı");
        }
    }

    public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
    {
        var userRoles = await GetRolesToUserAsync(name);
        if (!userRoles.Any())
            return false;

      Endpoint? endpoint = await endpointReadRepository.GetAsync(e => e.Code == code, include: r => r.Include(r => r.Roles));
        if(endpoint is null)
        {
            return false;
        }
        var hasRole = false;

        var endpointRoles = endpoint.Roles.Select(r => r.Name);

        foreach (var userRole in userRoles)
        {

            foreach (var endpointRole in endpointRoles)
                if (userRole == endpointRole)
                    return true;
        }
        return false;
    }
}
