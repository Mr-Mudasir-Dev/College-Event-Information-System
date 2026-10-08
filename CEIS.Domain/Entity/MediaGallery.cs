using CEIS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Entity
{
    public class MediaGallery : BaseEntity
    {
        public int EventId { get; set; }
        public MediaFileType FileType { get; set; }
        public string FileUrl { get; set; } = string.Empty;
        public string UploadedBy { get; set; } = string.Empty;
        public string? Caption { get; set; }
    }
}
