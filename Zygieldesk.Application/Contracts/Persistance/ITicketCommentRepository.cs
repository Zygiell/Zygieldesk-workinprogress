using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Contracts.Persistance
{
    public interface ITicketCommentRepository : IBaseRepository<TicketComment>
    {
        Task<IReadOnlyList<TicketComment>> GetAllTicketCommentsFromTicketAsync(int ticketId);
    }
}