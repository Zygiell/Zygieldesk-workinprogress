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
    }
}
