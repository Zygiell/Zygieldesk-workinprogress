using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Authorization
{
    public class UserResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, User>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, User resource)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
            var userEmail = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;

            if (requirement.ResourceOperation == ResourceOperation.Update ||
                requirement.ResourceOperation == ResourceOperation.Delete)
            {
                if (userEmail == resource.Email || userRole == "Admin")
                {
                    context.Succeed(requirement);
                }
            }
            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                if (userEmail == resource.Email ||
                    userRole == "Admin" ||
                    userRole == "Support")
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}