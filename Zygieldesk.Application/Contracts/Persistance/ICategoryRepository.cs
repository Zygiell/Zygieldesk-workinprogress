using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Contracts.Persistance
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategoryWithTickets(int categoryId);
    }
}