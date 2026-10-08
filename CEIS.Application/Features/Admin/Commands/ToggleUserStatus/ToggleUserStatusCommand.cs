using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommand : IRequest<Result<string>>
    {
        public string UserId { get; set; } = string.Empty;
        public bool Suspend { get; set; }
    }
}
