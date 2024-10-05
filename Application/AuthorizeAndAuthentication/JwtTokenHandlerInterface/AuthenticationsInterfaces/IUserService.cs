using Application.JwtTokenHandlerInterface.DTOs;
using Domain.BaseProjeEntities.IdentityEntities;

namespace Application.JwtTokenHandlerInterface.AuthenticationsInterfaces;

public interface IUserService
{
    Task<CreateUserResponseDTO> CreateAsync(CreateUserDTO model);

    Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);

    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);

    Task<List<ListUserDto>> GetAllUsersAsync();

    Task AssignRoleToUserAsync(string userId , string[] roles);

    Task<string[]> GetRolesToUserAsync(string userIdOrName);

    Task<bool> HasRolePermissionToEndpointAsync(string name, string code);
}
