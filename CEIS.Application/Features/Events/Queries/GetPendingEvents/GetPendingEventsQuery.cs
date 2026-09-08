using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Queries.GetPendingEvents
{
    public class GetPendingEventsQuery : IRequest<Result<IEnumerable<EventDto>>>
    {
    }
}
