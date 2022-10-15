using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Authorization
{
    public class CategoryResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Category>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Category resource)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                if (userRole == "Support")
                {
                    context.Succeed(requirement);
                }
            }

            if (userRole == "Admin")
            {
                context.Succeed(requirement);
            }




            return Task.CompletedTask;
        }
    }
}