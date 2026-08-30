using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Commands.Logout
{
    public class LogoutCommand : IRequest<Unit>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
