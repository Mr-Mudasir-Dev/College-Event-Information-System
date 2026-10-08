using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Feedbacks.Queries
{
    public class FeedbackDto
    {
        public int Rating { get; set; }
        public string? Comments { get; set; }
        public string UserName { get; set; } = string.Empty;
        public DateTime SubmittedOn { get; set; }
    }
}
