using CEIS.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<Result<CurrentUserDto>>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
