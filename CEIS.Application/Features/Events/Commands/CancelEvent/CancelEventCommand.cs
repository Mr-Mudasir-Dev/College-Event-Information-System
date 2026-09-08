using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.CancelEvent
{
    public class CancelEventCommand : IRequest<Result<string>>
    {
        public string OrganizerId { get; set; } = string.Empty;
        public int EventId { get; set; }
    }
}
