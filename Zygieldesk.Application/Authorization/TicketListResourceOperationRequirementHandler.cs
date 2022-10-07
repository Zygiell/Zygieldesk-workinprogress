using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Authorization
{
    public class TicketListResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, List<Ticket>>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, List<Ticket> resource)
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