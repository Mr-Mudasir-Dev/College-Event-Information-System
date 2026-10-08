using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Entity
{
    public class Certificate : BaseEntity
    {
        public int EventId { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string CertificateUrl { get; set; } = string.Empty;
        public DateTime IssuedOn { get; set; } = DateTime.UtcNow;
    }
}
