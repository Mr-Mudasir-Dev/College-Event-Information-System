using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.MediaGallery.Queries
{
    public class MediaDto
    {
        public int Id { get; set; }
        public string FileType { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public string? Caption { get; set; }
    }
}
