using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Queries.GetEventAttendance
{
    public class GetEventAttendanceQuery : IRequest<Result<IEnumerable<EventAttendanceDto>>>
    {
        public int EventId { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
    }
}
