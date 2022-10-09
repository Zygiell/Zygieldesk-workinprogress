using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Middlewares;
using Zygieldesk.Application.PipelineBehaviors;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application
{
    public static class ApplicationInstallation
    {
        public static IServiceCollection AddZygieldeskApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, CategoryResourceOperationRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, TicketResourceOperationRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, TicketListResourceOperationRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, TicketCommentResourceOperationRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, TicketCommentListResourceOperationRequirementHandler>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}