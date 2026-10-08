using CEIS.Application.Common;
using CEIS.Application.Features.Admin.Commands.AssignRole;
using CEIS.Application.Features.Admin.Commands.ToggleUserStatus;
using CEIS.Application.Features.Admin.Queries.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize( Roles = "Admin" )]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpPut("users/assign-role")]
        public async Task<IActionResult> AssignRole(AssignRoleCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPut("users/toggle-status")]
        public async Task<IActionResult> ToggleUserStatus(ToggleUserStatusCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
