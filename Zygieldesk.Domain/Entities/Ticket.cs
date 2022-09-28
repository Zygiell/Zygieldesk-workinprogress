using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public string TicketTitle { get; set; }
        public string TicketBody { get; set; }
        public int CategoryId { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime TicketCreationDate { get; set; }


    }
}
