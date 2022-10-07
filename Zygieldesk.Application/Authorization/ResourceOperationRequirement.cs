using Microsoft.AspNetCore.Authorization;

namespace Zygieldesk.Application.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete,
        Reply
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; set; }

        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
    }
}