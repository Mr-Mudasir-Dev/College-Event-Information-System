using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetMyEvents
{
    public class GetMyEventsCommand : IRequest<Result<IEnumerable<EventDto>>>
    {
        public string OrganizerId { get; set; } = string.Empty;
    }
}
