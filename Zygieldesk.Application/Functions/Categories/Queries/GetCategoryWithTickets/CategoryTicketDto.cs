using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets
{
    public class CategoryTicketDto
    {
        public int Id { get; set; }
        public string TicketTitle { get; set; }
        public string TicketBody { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime TicketCreationDate { get; set; }
        public int CategoryId { get; set; }

    }
}
