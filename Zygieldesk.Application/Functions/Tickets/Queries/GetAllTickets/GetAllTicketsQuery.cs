using MediatR;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetAllTickets
{
    public class GetAllTicketsQuery : IRequest<List<TicketViewModel>>
    {
    }
}