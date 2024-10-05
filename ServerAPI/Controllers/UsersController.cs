using Application.CustomAttributes;
using Application.Features.Auth.CreateAssignRoleToUser;
using Application.Features.Auth.Login;
using Application.Features.Auth.Register;
using Application.Interfaces.MailService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.AuthorizationFiters;

namespace ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IMailService _mailService;

        public UsersController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost]
        [ServiceFilter(typeof(RolePermissionFilter))]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = Application.CustomAttributes.Enums.ActionType.Reading, Definition = "Create User", Menu = "Users")]
        public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
        {
            var response = await Mediator.Send(createUserCommand);
            return Ok(response);
        }

        [HttpPost("Example")]
        [ServiceFilter(typeof(AuthenticationFilter))]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Example(CreateUserCommand createUserCommand)
        {
            var response = await Mediator.Send(createUserCommand);
            return Ok(response);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommand loginUserCommand)
        {
            var response = await Mediator.Send(loginUserCommand);
            return Ok(response);
        }

       

       

        

        [HttpPost("[action]")]
        //[Authorize(AuthenticationSchemes = "Admin")]
        //[AuthorizeDefinition(ActionType = Application.CustomAttributes.Enums.ActionType.Writing, Definition = "Assign Role To User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommand assignRoleToUser)
        {
            var response = await Mediator.Send(assignRoleToUser);
            return Ok(response);
        }
    }
}
