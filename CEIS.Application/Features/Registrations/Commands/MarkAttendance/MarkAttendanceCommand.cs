using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.MarkAttendance
{
    public class MarkAttendanceCommand : IRequest<Result<string>>
    {
        public int RegistrationId { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
    }
}
