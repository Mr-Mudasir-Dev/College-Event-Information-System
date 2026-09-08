using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.RejectEvent
{
    public class RejectEventCommand : IRequest<Result<string>>
    {
        public int EventId { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
