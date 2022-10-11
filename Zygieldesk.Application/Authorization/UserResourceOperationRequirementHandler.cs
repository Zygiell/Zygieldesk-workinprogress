using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

            if(requirement.ResourceOperation == ResourceOperation.Update)
            {
                if (userEmail == resource.Email)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;

        }
    }
}
