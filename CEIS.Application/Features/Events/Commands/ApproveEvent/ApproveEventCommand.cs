using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Events.Commands.ApproveEvent
{
    public class ApproveEventCommand : IRequest<Result<string>>
    {
        public int EventId { get; set; }
    }
}
