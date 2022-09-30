using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Persistance.Repositories
{
    public class TicketCommentRepository : BaseRepository<TicketComment>, ITicketCommentRepository
    {
        public TicketCommentRepository(ZygieldeskDbContext dbContext) : base(dbContext)
        {
        }
    }
}
