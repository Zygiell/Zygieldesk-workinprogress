using MediatR;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList
{
    public class GetTicketListFromCategoryQuery : IRequest<List<TicketListViewModel>>
    {
        public int CategoryId { get; set; }
    }
}