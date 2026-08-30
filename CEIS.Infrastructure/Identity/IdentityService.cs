using CEIS.Application.Common.Models;
using CEIS.Application.Features.Auth.Queries;
using CEIS.Application.Interfaces;
using CEIS.Domain.Entity;
using CEIS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public IdentityService(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AuthResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "User not founded!" }
                };

            var result = await _userManager.ChangePasswordAsync
                (user, currentPassword, newPassword);

            if (!result.Succeeded)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };

            return new AuthResult
            {
                Succeeded = true,
            };
        }

        public async Task<string> GenerateRefreshTokenAsync(string userId)
        {
            var existingTokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == userId)
                .ToListAsync();

            if (existingTokens.Any())
                _context.RefreshTokens.RemoveRange(existingTokens);

            var refreshToken = new RefreshToken
            {
                Token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N"),
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                CreatedAt = DateTime.UtcNow
            };

            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return refreshToken.Token;
        }

        public async Task<CurrentUserDto?> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            var dto = new CurrentUserDto
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                UserName = user.UserName!,
                Department = user.Department,
                EnrollmentNo = user.EnrollmentNo,
                Roles = roles
            };

            return dto;
        }

        public async Task<AuthResult> LoginAsync(string EmailOrUserName, string password)
        {
            var user = await _userManager.FindByEmailAsync(EmailOrUserName)
                ?? await _userManager.FindByNameAsync(EmailOrUserName);

            if (user == null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Invalid email or password." }
                };
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);

            if (!isPasswordValid)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Invalid email or password." }
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResult
            {
                Succeeded = true,
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Roles = roles
            };

        }

        public async Task<AuthResult> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedToken == null || !storedToken.IsActive)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "Invalid or expired refresh token." }
                };
            }

            var user = await _userManager.FindByIdAsync(storedToken.UserId);
            if (user == null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            // Purana refresh token revoke karo (rotation — security best practice)
            _context.RefreshTokens.Remove(storedToken);
            await _context.SaveChangesAsync();

            return new AuthResult
            {
                Succeeded = true,
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Roles = roles
            };
        }

        public async Task<AuthResult> RegisterAsync(string fullName, string userName, string email, string password, string department, string? enrollmentNo, string role)
        {
            var existingUserEmail = await _userManager.FindByEmailAsync(email);
            if (existingUserEmail != null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "An account with this email already exists." }
                };
            }

            var existingUser = await _userManager.FindByNameAsync(userName);
            if (existingUser != null)
            {
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = new List<string> { "This username is already taken." }
                };
            }

            if (!string.IsNullOrWhiteSpace(enrollmentNo))
            {
                var enrollmentExists = await
                    _context.Users.OfType<ApplicationUser>().AnyAsync(u => u.EnrollmentNo == enrollmentNo);

                if (enrollmentExists)
                    return new AuthResult
                    {
                        Succeeded = false,
                        Errors = new List<string> { "This enrollment number is already registered." }
                    };
            }

            var user = new ApplicationUser
            {
                FullName = fullName,
                Email = email,
                UserName = userName,
                Department = department,
                EnrollmentNo = enrollmentNo,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                return new AuthResult
                {
                    Succeeded = false,
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };

            await _userManager.AddToRoleAsync(user, role);

            return new AuthResult
            {
                Succeeded = true,
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Roles = new List<string> { role }
            };

        }

        public async Task RevokeRefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

            if (storedToken != null)
            {
                _context.RefreshTokens.Remove(storedToken);
                await _context.SaveChangesAsync();
            }
        }
    }
}
