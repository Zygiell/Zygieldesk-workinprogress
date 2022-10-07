using MediatR;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList
{
    public class GetTicketCommentListFromTicketQuery : IRequest<List<TicketCommentListViewModel>>
    {
        public int TicketId { get; set; }
    }
}