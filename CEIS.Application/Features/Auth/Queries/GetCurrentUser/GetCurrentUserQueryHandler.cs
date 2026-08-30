using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using CEIS.Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Queries.GetCurrentUser
{
    public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<CurrentUserDto>>
    {
        private readonly IIdentityService _identityService;
        public GetCurrentUserQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<CurrentUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserByIdAsync(request.UserId);

            if (user == null)
                throw new CEIS.Domain.Exceptions.NotFoundException("ApplicationUser", request.UserId);

            return Result<CurrentUserDto>.Success(user);
        }
    }
}
