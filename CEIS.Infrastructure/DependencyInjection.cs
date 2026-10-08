using CEIS.Application.Interfaces;
using CEIS.Application.Interfaces.Repositories;
using CEIS.Infrastructure.Data;
using CEIS.Infrastructure.Data.Repositories;
using CEIS.Infrastructure.Identity;
using CEIS.Infrastructure.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;

namespace CEIS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services,
            IConfiguration configuration)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            
            services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddScoped<IFileStorageService, LocalFileStorageService>();
            services.AddScoped<ICertificateGenerator, CertificateGenerator>();

            // Repositories
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IRegistrationRepository, RegistrationRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepositoy>();
            services.AddScoped<IMediaGalleryRepository, MediaGalleryRepository>();
            services.AddScoped<ICertificateRepository, CertificateRepository>();

            return services;
        }
    }
}
