using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Commands.ToggleUserStatus
{
    public class ToggleUserStatusCommandHandler : IRequestHandler<ToggleUserStatusCommand, Result<string>>
    {
        private readonly IIdentityService _identity;
        public ToggleUserStatusCommandHandler(IIdentityService identity)
        {
            _identity = identity;
        }

        public async Task<Result<string>> Handle(ToggleUserStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await _identity.ToggleUserStatusAsync(request.UserId, request.Suspend);

            if (!result.Succeeded)
                return Result<string>.Failure(result.Errors);

            var msg = request.Suspend ? "User account suspended." : "User account activated.";

            return Result<string>.Success(msg);
        }
    }
}
