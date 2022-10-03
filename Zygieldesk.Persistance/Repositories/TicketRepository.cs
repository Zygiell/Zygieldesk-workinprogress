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
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ZygieldeskDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Ticket>> GetAllTicketsFromCategoryAsync(int categoryId)
        {
            var category = await _dbContext.Categories
                .Include(t=>t.Tickets)
                .FirstOrDefaultAsync(c=>c.Id == categoryId);
            
            
            if(category!=null)
            {
                return category.Tickets.ToList();

            }

            return null;
        }

        public async Task<Ticket> GetTicketByIdWithTicketComments(int ticketid)
        {
            var ticket = await _dbContext.Tickets
                .Include(t => t.TicketComments)
                .FirstOrDefaultAsync(ti=>ti.Id == ticketid);

            return ticket;
        }
    }
}
