namespace Application.JwtTokenHandlerInterface.AuthenticationsInterfaces
{
    public interface IAuthService
    {
        Task FacebookLoginAsync();
        Task<Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
        Task TwitterLoginAsync();

        Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
        Task<Token> RefreshTokenLoginAsync(string refreshToken);

        Task PasswordResetAsync(string email);
        Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    }
}
