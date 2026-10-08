using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserListDto>>>
    {
        private readonly IIdentityService _identity;
        public GetAllUsersQueryHandler(IIdentityService identity)
        {
            _identity = identity;
        }
        public async Task<Result<IEnumerable<UserListDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _identity.GetAllUsersAsync();
            return Result<IEnumerable<UserListDto>>.Success(users);
        }
    }
}
