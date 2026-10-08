using CEIS.Application.Common;
using CEIS.Application.Features.Certificates.Commands.GenerateCertificate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CertificatesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("events/{eventId}")]
        [Authorize(Roles = "Participant")]
        public async Task<IActionResult> GenerateCertificate(int eventId)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _mediator.Send(new GenerateCertificateCommand
            {
                EventId = eventId,
                StudentId = studentId!
            });

            return Ok(ApiResponse<object>.SuccessResponse(result.Data, "Certificate ready for download."));
        }
    }
}
