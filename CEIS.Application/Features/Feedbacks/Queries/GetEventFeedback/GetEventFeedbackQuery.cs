using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Feedbacks.Queries.GetEventFeedback
{
    public class GetEventFeedbackQuery : IRequest<Result<IEnumerable<FeedbackDto>>>
    {
        public int EventId { get; set; }
    }
}
