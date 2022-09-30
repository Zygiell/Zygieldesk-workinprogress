using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Domain.Common;

namespace Zygieldesk.Domain.Entities
{
    public class Ticket : AuditableEntity
    {
        public int Id { get; set; }
        public string TicketTitle { get; set; }
        public string TicketBody { get; set; }
        public int CategoryId { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Open;


    }
}
