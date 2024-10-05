
using Application.Features.Auth.CreateAssignRoleToEndpoint;
using Microsoft.AspNetCore.Mvc;

namespace ServerAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorizationEndpointsController : BaseController
{
    

    [HttpPost]
    public async Task<IActionResult> AuthorizationEndpoints([FromBody]AssignRoleCommand assignRoleCommand)
    {
        assignRoleCommand.Type = typeof(Program);
        var response = await Mediator.Send(assignRoleCommand);
        return Ok(response);
    }
}
