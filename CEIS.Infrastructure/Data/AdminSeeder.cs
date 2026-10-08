using CEIS.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Infrastructure.Data
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            var adminSection = configuration.GetSection("DefaultAdmin");
            var email = adminSection["Email"];
            var userName = adminSection["UserName"];
            var password = adminSection["Password"];
            var fullName = adminSection["FullName"];

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return;

            var existingAdmin = await userManager.FindByEmailAsync(email);

            if(existingAdmin != null) return;

            var adminUser = new ApplicationUser
            {
                FullName = fullName ?? "System Administrator",
                Email = email,
                UserName = userName ?? email,
                Department = "Administration",
                EnrollmentNo = null,
                CreatedAt = DateTime.UtcNow,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, password);

            if(result.Succeeded)
                await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
