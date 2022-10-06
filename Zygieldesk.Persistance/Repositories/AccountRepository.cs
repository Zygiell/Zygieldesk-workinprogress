using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Repositories
{
    public class AccountRepository : BaseRepository<User>, IAccountRepository
    {
        public AccountRepository(ZygieldeskDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.Include(u=>u.Role).FirstOrDefaultAsync(u=> u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<int> GetUserRoleId()
        {
            var userRole = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "User");
            return userRole.Id;
        }

        public async Task<bool> IsEmailAddressFree(string email)
        {
            var isEmailTaken = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if(isEmailTaken == null)
            {
                return true;
            }
            return false;
        }

    }
}
