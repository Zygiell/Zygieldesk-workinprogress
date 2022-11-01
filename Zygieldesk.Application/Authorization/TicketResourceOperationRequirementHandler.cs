using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Authorization
{
    public class TicketResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Ticket>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Ticket resource)
        {
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var userRole = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;

            if (requirement.ResourceOperation == ResourceOperation.Read)
            {
                if (int.Parse(userId) == resource.CreatedByUserId ||
                    userRole == "Support")
                {
                    context.Succeed(requirement);
                }
            }
            if (requirement.ResourceOperation == ResourceOperation.Reply &&
                resource.Status != TicketStatus.Closed)
            {
                if (int.Parse(userId) == resource.CreatedByUserId ||
                    userRole == "Support")
                {
                    context.Succeed(requirement);
                }
            }

            if (requirement.ResourceOperation == ResourceOperation.Delete)
            {
                if (int.Parse(userId) == resource.CreatedByUserId && resource.Status == TicketStatus.Open)
                {
                    context.Succeed(requirement);
                }
            }
            if(requirement.ResourceOperation == ResourceOperation.SetPriority)
            {
                if(userRole == "Support")
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