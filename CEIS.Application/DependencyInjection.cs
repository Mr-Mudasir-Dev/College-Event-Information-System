using CEIS.Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CEIS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // MediatR ko is assembly ke saare handlers register karne ko bolo
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            // FluentValidation validators is assembly se register karo
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            // FluentValidation validators Pipline
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // AutoMapper profiles register karo
            services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());


            return services;
        }
    }
}
