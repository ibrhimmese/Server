using Application.JwtTokenHandlerInterface.AuthenticationsInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

     //[Authorize(AuthenticationSchemes ="Admin")]
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAllRolesAsync();
            return Ok(result);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetRoleById(Guid id)
        {
            var result =await _roleService.GetRoleByIdAsync(id);
            return Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateRole(string name)
        {
            var result = await _roleService.CreateRoleAsync(name);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole(Guid id, string name)
        {
            var result = await _roleService.UpdateRoleAsync(id, name);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var result = await _roleService.DeleteRoleAsync(id);
            return Ok(result);
        }
    }
}
