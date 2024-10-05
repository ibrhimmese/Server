
using Application.JwtTokenHandlerInterface.DTOs;

namespace Application.Features.Auth.GetUsers;
public class UserListQueryCommandResponse
{
    public List<ListUserDto> UserList { get; set; }
}