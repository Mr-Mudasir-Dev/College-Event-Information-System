using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Commands.ChangePassword
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<string>>
    {
        private readonly IIdentityService _identityService;
        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<Result<string>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.ChangePasswordAsync
                (request.UserId, request.CurrentPassword, request.NewPassword);

            if(!result.Succeeded)
                return Result<string>.Failure(result.Errors);

            return Result<string>.Success("Password changed successfully.");
        }
    }
}
