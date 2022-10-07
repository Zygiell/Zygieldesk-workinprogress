using MediatR;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById
{
    public class GetTicketCommentByIdQuery : IRequest<TicketCommentViewModel>
    {
        public int TicketCommentId { get; set; }
    }
}