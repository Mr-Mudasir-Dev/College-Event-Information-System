using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<Result<AuthResult>>
    {
        public string EmailOrUserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
