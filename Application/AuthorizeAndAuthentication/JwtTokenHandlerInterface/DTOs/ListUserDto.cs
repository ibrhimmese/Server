namespace Application.JwtTokenHandlerInterface.DTOs;

public class ListUserDto
{
    public Guid Id { get; set; }
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool TwoFactorEnabled { get; set; }
}

        
          
       