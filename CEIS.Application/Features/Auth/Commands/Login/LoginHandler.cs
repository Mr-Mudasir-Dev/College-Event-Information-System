using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result<AuthResult>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IIdentityService identityService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _identityService = identityService;
        }

        public async Task<Result<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.LoginAsync(request.EmailOrUserName, request.Password);

            if(!result.Succeeded)
                return Result<AuthResult>.Failure(result.Errors);

            var tokken = await _jwtTokenGenerator
                .GenerateTokenAsync(
                result.UserId!,
                result.Email!,
                result.FullName!,
                result.Roles);

            var refreshToken = await _identityService.GenerateRefreshTokenAsync(result.UserId!);

            result.Token = tokken;
            result.RefreshToken = refreshToken;

            return Result<AuthResult>.Success(result);
        }
    }
}
