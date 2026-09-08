using CEIS.Application.Common.Models;
using CEIS.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetEvents
{
    public class GetEventsQuery : IRequest<Result<IEnumerable<EventDto>>>
    {
        public EventCategory? Category { get; set; }
        public DateOnly? FromDate { get; set; }
        public DateOnly? ToDate { get; set; }
    }
}
