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
    public class TicketCommentListResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, List<TicketComment>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, List<TicketComment> resource)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                if (userRole == "Support" || userRole == "Admin")
                {
                    context.Succeed(requirement);
                }
            }





            return Task.CompletedTask;
        }
    }
}
