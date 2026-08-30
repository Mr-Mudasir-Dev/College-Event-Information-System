using CEIS.Application.Common.Models;
using CEIS.Application.Features.Auth.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthResult> RegisterAsync(
            string fullName,
            string userName,
            string email,
            string password,
            string department,
            string? enrollmentNo,
            string role
            );

        Task<AuthResult> LoginAsync(string emailOrUserName, string password);
        Task<CurrentUserDto?> GetUserByIdAsync(string userId);
        Task<AuthResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
        Task<string> GenerateRefreshTokenAsync(string userId);
        Task<AuthResult> RefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}
