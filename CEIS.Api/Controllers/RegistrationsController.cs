using CEIS.Application.Common;
using CEIS.Application.Features.Registrations.Commands.CancelRegistration;
using CEIS.Application.Features.Registrations.Commands.RegisterForEvent;
using CEIS.Application.Features.Registrations.Queries.GetEventRegistrations;
using CEIS.Application.Features.Registrations.Queries.GetMyRegistrations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RegistrationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("events/{eventId}")]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> RegisterForEvent(int eventId)
        {
            var Participant = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _mediator.Send(new RegisterForEventCommand
            {
                EventId = eventId,
                StudentId = Participant
            });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("events/{eventId}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetEventRegistrations(int eventId)
        {
            var organizerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new GetEventRegistrationsQuery
            {
                EventId = eventId,
                OrganizerId = organizerId!
            });

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpDelete("events/{eventId}")]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> CancelRegistration(int eventId)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new CancelRegistrationCommand
            {
                EventId = eventId,
                StudentId = studentId!
            });

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("my-registrations")]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> GetMyRegistrations()
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetMyRegistrationsQuery 
            { 
                StudentId = studentId!
            });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
