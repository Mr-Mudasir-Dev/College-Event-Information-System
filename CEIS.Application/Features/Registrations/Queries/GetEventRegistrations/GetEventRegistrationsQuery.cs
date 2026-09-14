using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Queries.GetEventRegistrations
{
    public class GetEventRegistrationsQuery : IRequest<Result<IEnumerable<EventRegistrationDto>>>
    {
        public int EventId { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
    }
}
