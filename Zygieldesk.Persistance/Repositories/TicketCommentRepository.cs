using Microsoft.EntityFrameworkCore;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Repositories
{
    public class TicketCommentRepository : BaseRepository<TicketComment>, ITicketCommentRepository
    {
        public TicketCommentRepository(ZygieldeskDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<TicketComment>> GetAllTicketCommentsFromTicketAsync(int ticketId)
        {
            var ticket = await _dbContext.Tickets
                .Include(t => t.TicketComments)
                .FirstOrDefaultAsync(ti => ti.Id == ticketId);

            if (ticket != null)
            {
                return ticket.TicketComments.ToList();
            }

            return null;
        }
    }
}