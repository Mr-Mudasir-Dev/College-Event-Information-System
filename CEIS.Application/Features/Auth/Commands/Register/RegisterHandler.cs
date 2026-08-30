using CEIS.Application.Common.Models;
using CEIS.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Features.Auth.Commands.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Result<AuthResult>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public RegisterHandler(
            IJwtTokenGenerator jwtTokenGenerator,
            IIdentityService identityService)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _identityService = identityService;
        }
        public async Task<Result<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.RegisterAsync(
                request.FullName,
                request.UserName,
                request.Email,
                request.Password,
                request.Department,
                request.EnrollmentNo,
                role: "Participant"
            );

            if (!result.Succeeded)
                return Result<AuthResult>.Failure(result.Errors);

            var token = await _jwtTokenGenerator.GenerateTokenAsync(
                result.UserId!,
                result.Email!,
                result.FullName!,
                result.Roles);

            var refreshToken = await _identityService.GenerateRefreshTokenAsync(result.UserId!);

            result.Token = token;
            result.RefreshToken = refreshToken;

            return Result<AuthResult>.Success(result);
        }
    }
}
