using CEIS.Application.Common.Models;
using CEIS.Domain.Enum;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.FilterEvents
{
    public class FilterEventsQuery : IRequest<Result<IEnumerable<EventDto>>>
    {
        public string OrganizerId { get; set; } = string.Empty;
        public string? titleVenue { get; set; }
        public string? Date { get; set; }
        public string? Category { get; set; }
        public string? Status { get; set; }
    }
}
