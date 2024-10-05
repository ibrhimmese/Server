using Application.ExceptionTypes;
using Application.JwtTokenHandlerInterface;
using Domain.BaseProjeEntities.IdentityEntities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Infrastructure.JwtTokenHandlerConcretes;

public class TokenHandler(IConfiguration configuration) : ITokenHandler
{
    public Token CreateAccessToken(int minute, AppUser user)
    {
        Token token = new();


        string? securityKeyString = configuration["Token:SecurityKey"];
        if (string.IsNullOrEmpty(securityKeyString))
        {
            throw new OperationException("SecurityKey is not configured.");
        }

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(securityKeyString));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        token.Expiration = DateTime.UtcNow.AddDays(minute);

        JwtSecurityToken securityToken = new(
            audience: configuration["Token:Audience"],
            issuer: configuration["Token:Issuer"],
            expires: token.Expiration,
            notBefore: DateTime.UtcNow,
            signingCredentials: signingCredentials,
            claims:new List<Claim> { new(ClaimTypes.Name, user.UserName!)}
            );

        JwtSecurityTokenHandler tokenHandler = new();
        token.AccessToken = tokenHandler.WriteToken(securityToken);

        token.RefreshToken = CreateRefreshToken();

        return token;

    }

    public string CreateRefreshToken()
    {
        byte[] number = new byte[32];
        using RandomNumberGenerator random = RandomNumberGenerator.Create();
        random.GetBytes(number);
        return Convert.ToBase64String(number);
    }
}
