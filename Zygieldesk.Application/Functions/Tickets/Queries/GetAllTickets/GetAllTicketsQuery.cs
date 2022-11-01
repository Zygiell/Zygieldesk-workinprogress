using MediatR;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetAllTickets
{
    public class GetAllTicketsQuery : IRequest<List<TicketListViewModel>>
    {
    }
}