using Application.JwtTokenHandlerInterface.DTOs;

namespace Application.JwtTokenHandlerInterface.AuthenticationsInterfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRoleAsync(string name);
        Task<bool> DeleteRoleAsync(Guid id);
        Task<bool> UpdateRoleAsync(Guid id, string name);
        Task<List<AppRoleDto>> GetAllRolesAsync();
        Task<AppRoleDto> GetRoleByIdAsync(Guid id);

    }
}
