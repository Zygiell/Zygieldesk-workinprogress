using MediatR;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments
{
    public class GetAllTicketsCommentsQuery : IRequest<List<TicketCommentViewModel>>
    {
    }
}