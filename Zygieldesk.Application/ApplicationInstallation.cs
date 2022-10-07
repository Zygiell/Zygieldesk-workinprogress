using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Authentication;
using Zygieldesk.Domain.Entities;
using Zygieldesk.Application.Services;
using Zygieldesk.Application.Middlewares;

namespace Zygieldesk.Application
{
    public static class ApplicationInstallation
    {
        public static IServiceCollection AddZygieldeskApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, CategoryResourceOperationRequirementHandler>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
