using CEIS.Application.Common;
using CEIS.Application.Features.MediaGallery.Commands.DeleteMedia;
using CEIS.Application.Features.MediaGallery.Commands.UploadMedia;
using CEIS.Application.Features.MediaGallery.Queries.GetEventMedia;
using CEIS.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic.FileIO;
using System.Security.Claims;

namespace CEIS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MediaGalleryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MediaGalleryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("events/{eventId}")]
        [Authorize(Roles = "Organizer,Admin")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadMedia(int eventId, IFormFile file, MediaFileType fileType, string? caption)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var cmd = new UploadMediaCommand
            {
                EventId = eventId,
                File = file,
                FileType = fileType,
                Caption = caption,
                UploadedBy = userId!,
                IsAdmin = isAdmin
            };

            var result = await _mediator.Send(cmd);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Media uploaded successfully."));
        }

        [HttpGet("events/{eventId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventMedia(int eventId)
        {
            var result = await _mediator.Send(new GetEventMediaQuery());
            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }

        [HttpDelete("{mediaId}")]
        [Authorize(Roles = "Organizer,Admin")]
        public async Task<IActionResult> DeleteMedia(int mediaId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = User.IsInRole("Admin");

            var result = await _mediator.Send(new DeleteMediaCommand
            {
                MediaId = mediaId,
                IsAdmin = isAdmin,
                RequestedBy = userId!
            });

            return Ok(ApiResponse<object>.SuccessResponse(result.Data));
        }
    }
}
