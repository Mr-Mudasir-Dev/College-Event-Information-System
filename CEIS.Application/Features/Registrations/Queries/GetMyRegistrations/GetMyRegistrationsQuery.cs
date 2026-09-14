using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Queries.GetMyRegistrations
{
    public class GetMyRegistrationsQuery : IRequest<Result<IEnumerable<RegistrationDto>>>
    {
        public string StudentId { get; set; } = string.Empty;
    }
}
