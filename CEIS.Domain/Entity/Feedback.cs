using CEIS.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Domain.Entity
{
    public class Feedback : BaseEntity
    {
        public string UserId { get; set; } = string.Empty;
        public FeedbackCategory Category { get; set; }
        public int? EventId { get; set; }
        public int Rating { get; set; }
        public string? Comments { get; set; }
    }
}
