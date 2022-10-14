using Microsoft.EntityFrameworkCore;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ZygieldeskDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Category> GetCategoryWithTickets(int categoryId)
        {
            return (await _dbContext
                .Categories
                .Include(t => t.Tickets)
                .FirstOrDefaultAsync(c => c.Id == categoryId));
        }
    }
}