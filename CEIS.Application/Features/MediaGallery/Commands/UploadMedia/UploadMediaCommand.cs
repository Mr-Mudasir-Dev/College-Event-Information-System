using CEIS.Application.Common.Models;
using CEIS.Domain.Enum;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Commands.UploadMedia
{
    public class UploadMediaCommand : IRequest<Result<string>>
    {
        public int EventId { get; set; }
        public IFormFile File { get; set; } = null!;
        public MediaFileType FileType { get; set; }
        public string? Caption { get; set; }
        public string UploadedBy { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
    }
}
