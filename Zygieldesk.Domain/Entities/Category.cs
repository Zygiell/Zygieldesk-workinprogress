using Zygieldesk.Domain.Common;

namespace Zygieldesk.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}