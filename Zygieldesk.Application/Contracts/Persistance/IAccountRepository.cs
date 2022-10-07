using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Contracts.Persistance
{
    public interface IAccountRepository : IBaseRepository<User>
    {
        Task<bool> IsEmailAddressFree(string email);

        Task<int> GetUserRoleId();

        Task<User> GetUserByEmail(string email);
    }
}