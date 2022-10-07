using MediatR;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<TicketViewModel>
    {
        public int TicketId { get; set; }
    }
}