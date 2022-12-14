using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string TicketTitle { get; set; }
        public string TicketBody { get; set; }
        public int CategoryId { get; set; }
        public TicketStatus Status { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public ICollection<TicketComment> TicketComments { get; set; }
    }
}