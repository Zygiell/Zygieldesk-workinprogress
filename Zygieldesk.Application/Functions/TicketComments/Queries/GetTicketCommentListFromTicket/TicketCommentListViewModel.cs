namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList
{
    public class TicketCommentListViewModel
    {
        public int Id { get; set; }
        public string CommentBody { get; set; }
        public int TicketId { get; set; }
        public int? CreatedByUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? LastModifiedByUserId { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}