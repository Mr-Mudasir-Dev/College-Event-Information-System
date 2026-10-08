using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Feedbacks.Commands.SubmitAttendanceFeedback
{
    public class SubmitAttendanceFeedbackCommand : IRequest<Result<string>>
    {
        public int EventId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int Rating { get; set; }
        public string? Comments { get; set; }
    }
}
