using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Admin.Commands.AssignRole
{
    public class AssignRoleCommandHandler : IRequestHandler<AssignRoleCommand, Result<string>>
    {
        private readonly IIdentityService _identity;

        public AssignRoleCommandHandler(IIdentityService identity)
        {
            _identity = identity;                   
        }
        public async Task<Result<string>> Handle(AssignRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _identity.AssignRoleAsync(request.UserId, request.NewRole);

            if (!result.Succeeded)
                return Result<string>.Failure(result.Errors);
            
            return Result<string>.Success($"Role updated to '{request.NewRole}' successfully.");
        }
    }
}
