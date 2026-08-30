using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, Result<AuthResult>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public RefreshTokenCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IIdentityService identityService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _identityService = identityService;
        }

        public async Task<Result<AuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RefreshTokenAsync(request.RefreshToken);

            if (!result.Succeeded)
            {
                return Result<AuthResult>.Failure(result.Errors);
            }

            var newAccessToken = await _jwtTokenGenerator.GenerateTokenAsync(
                result.UserId!, result.Email!, result.FullName!, result.Roles);

            var newRefreshToken = await _identityService.GenerateRefreshTokenAsync(result.UserId!);

            result.Token = newAccessToken;
            result.RefreshToken = newRefreshToken;

            return Result<AuthResult>.Success(result);
        }
    }
}
