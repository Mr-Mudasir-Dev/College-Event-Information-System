using CEIS.Application.Common;
using CEIS.Application.Features.Feedbacks.Commands.SubmitAttendanceFeedback;
using CEIS.Application.Features.Feedbacks.Queries.GetEventFeedback;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FeedbackController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("events/{eventId}")]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> SubmitAttendanceFeedback(int eventId, SubmitAttendanceFeedbackCommand cmd)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            cmd.UserId = UserId!;
            cmd.EventId = eventId;

            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result.Data, result.Message));
        }

        [HttpGet("events/{eventId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventFeedback(int eventId)
        {
            var result = await _mediator.Send(new GetEventFeedbackQuery
            {
                EventId = eventId
            });

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
