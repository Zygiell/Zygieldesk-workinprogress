using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Contracts.Persistance
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<IReadOnlyList<Ticket>> GetAllTicketsFromCategoryAsync(int categoryId);

        Task<Ticket> GetTicketByIdWithTicketComments(int ticketId);
    }
}