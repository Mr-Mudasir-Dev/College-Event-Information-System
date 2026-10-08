using CEIS.Application.Common;
using CEIS.Application.Features.Registrations.Commands.MarkAttendance;
using CEIS.Application.Features.Registrations.Queries.GetEventAttendance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("mark")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> MarkAttendance(MarkAttendanceCommand cmd)
        {
            var OrganizerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            cmd.OrganizerId = OrganizerId!;

            var result = await _mediator.Send(cmd);

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("events/{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetEventAttendance(int eventId)
        {
            var OrganizerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new GetEventAttendanceQuery
            {
                EventId = eventId,
                OrganizerId = OrganizerId!
            });

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }
    }
}
