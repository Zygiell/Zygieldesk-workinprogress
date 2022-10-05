using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Contracts.Persistance
{
    public interface IAccountRepository : IBaseRepository<User>
    {
        Task<bool> IsEmailAddressFree(string email);
        Task<int> GetUserRoleId();

    }
}
