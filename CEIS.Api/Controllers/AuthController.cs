using CEIS.Application.Common;
using CEIS.Application.Features.Auth.Commands.ChangePassword;
using CEIS.Application.Features.Auth.Commands.Login;
using CEIS.Application.Features.Auth.Commands.Logout;
using CEIS.Application.Features.Auth.Commands.RefreshToken;
using CEIS.Application.Features.Auth.Commands.Register;
using CEIS.Application.Features.Auth.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand cmd)
        {
            var result = await _mediator.Send(cmd);
            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));

            return Ok(ApiResponse<object>
                .SuccessResponse(result.Data, "Registration successful."));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand cmd)
        {
            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
            {
                return BadRequest(ApiResponse<object>.ValidationResponse(result.Errors));
            }

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, "Login successful."));
        }

        [HttpPost("me")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized(ApiResponse<object>.UnauthorizedResponse());

            var result = await _mediator.Send(new GetCurrentUserQuery{
                UserId = userId
            });

            if (!result.IsSuccess)
            {
                return NotFound(ApiResponse<object>.NotFoundResponse(result.Errors));
            }

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, "Login successful"));
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand cmd)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
                return Unauthorized(ApiResponse<object>.UnauthorizedResponse("Invalid token."));

            cmd.UserId = userId;

            var result = await _mediator.Send(cmd);

            if (!result.IsSuccess)
                return BadRequest(ApiResponse<object>.FailResponse(result.Errors));

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return Unauthorized(ApiResponse<object>.FailResponse(result.Errors));
            }

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(LogoutCommand cmd)
        {
            await _mediator.Send(cmd);
            return Ok(ApiResponse<Object>.SuccessResponse(null, "Logged out successfully."));
        }
    }
}
