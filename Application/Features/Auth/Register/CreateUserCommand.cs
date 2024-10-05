using Application.JwtTokenHandlerInterface.AuthenticationsInterfaces;
using Application.JwtTokenHandlerInterface.DTOs;
using Application.Pipelines;
using MediatR;

namespace Application.Features.Auth.Register;

public class CreateUserCommand : IRequest<CreatedUserResponse>, ILoggableRequest
{
    public string NameSurname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PasswordConfirm { get; set; }
    public CreateUserCommand(string nameSurname, string username, string email, string password, string passwordConfirm)
    {
        NameSurname = nameSurname;
        Username = username;
        Email = email;
        Password = password;
        PasswordConfirm = passwordConfirm;
    }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
{
    private readonly IUserService userService;

    public CreateUserCommandHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        CreateUserResponseDTO response = await userService.CreateAsync(new()
        {
            Email = request.Email,
            NameSurname = request.NameSurname,
            Password = request.Password,
            PasswordConfirm = request.PasswordConfirm,
            Username = request.Username,
        });
        return new CreatedUserResponse()
        {
            Message = response.Message,
            Succeeded = response.Succeeded
        };
    }
}
