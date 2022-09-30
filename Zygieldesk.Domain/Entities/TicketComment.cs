using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Domain.Common;

namespace Zygieldesk.Domain.Entities
{
    public class TicketComment : AuditableEntity
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public int TicketId { get; set; }
    }
}
