using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Registrations.Commands.CancelRegistration
{
    public class CancelRegistrationCommand : IRequest<Result<string>>
    {
        public int EventId { get; set; }
        public string StudentId { get; set; } = string.Empty;
    }
}
