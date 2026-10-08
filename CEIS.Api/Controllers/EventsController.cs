using CEIS.Application.Common;
using CEIS.Application.Features.Events.Commands.ApproveEvent;
using CEIS.Application.Features.Events.Commands.CancelEvent;
using CEIS.Application.Features.Events.Commands.CreateEvent;
using CEIS.Application.Features.Events.Commands.EventDelete;
using CEIS.Application.Features.Events.Commands.RejectEvent;
using CEIS.Application.Features.Events.Commands.UpdateEvent;
using CEIS.Application.Features.Events.Queries.GetEventById;
using CEIS.Application.Features.Events.Queries.GetEvents;
using CEIS.Application.Features.Events.Queries.GetMyEvents;
using CEIS.Application.Features.Events.Queries.GetPendingEvents;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class EventsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Authorize(Roles = "Organizer")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateEvent(CreateEventCommand cmd)
        {
            var organizerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(organizerId))
                return Unauthorized(ApiResponse<object>.UnauthorizedResponse());
            
            cmd.OrganizerId = organizerId;

            var result = await _mediator.Send(cmd);

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, "Event created successfully. Awaiting admin approval."));

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvent(GetEventsQuery cmd)
        {
            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data, "Fetched successfully"));
        }

        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveEvent(int id)
        {
            var result = await _mediator.Send(new ApproveEventCommand { EventId = id });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPut("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectEvent(int id, RejectEventCommand cmd)
        {
            cmd.EventId = id;
            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("pending")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPendingEvents()
        {
            var result = await _mediator.Send(new GetPendingEventsQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _mediator.Send(new GetEventByIdQuery { EventId = id });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Organizer")]
        public async Task<IActionResult> EventDelete(int id)
        {
            var result = await _mediator.Send(new EventDeleteCommand { EventId = id });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpGet("my-events")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> GetMyEvents()
        {
            var Organizer = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new GetMyEventsCommand { OrganizerId = Organizer! });
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> UpdateEvent(int id, UpdateEventCommand cmd)
        {
            cmd.EventId = id;
            cmd.OrganizerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpPut("{id}/cancel")]
        [Authorize(Roles = "Organizer")]
        public async Task<IActionResult> CancelEvent(int id)
        {
            var Organizer = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new CancelEventCommand { OrganizerId = Organizer!, EventId = id});
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
