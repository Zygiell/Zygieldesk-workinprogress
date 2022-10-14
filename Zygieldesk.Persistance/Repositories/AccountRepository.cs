using Microsoft.EntityFrameworkCore;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Repositories
{
    public class AccountRepository : BaseRepository<User>, IAccountRepository
    {
        public AccountRepository(ZygieldeskDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Role>> GetAllRoles()
        {
            var rolesList = await _dbContext.Roles.ToListAsync();

            return rolesList;
        }

        public async Task<Role> GetRoleById(int id)
        {
            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
            return role;
        }

        public async Task<IReadOnlyList<User>> GetAllUsersWithRoles()
        {
            var userList = await _dbContext.Users.Include(u => u.Role).ToListAsync();
            return userList;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
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
            if (isEmailTaken == null)
            {
                return true;
            }
            return false;
        }
    }
}