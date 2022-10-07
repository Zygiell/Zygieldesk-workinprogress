using System.Security.Claims;

namespace Zygieldesk.Application.Services
{
    public interface IUserContextService
    {
        int? GetUserId { get; }
        ClaimsPrincipal User { get; }
    }
}