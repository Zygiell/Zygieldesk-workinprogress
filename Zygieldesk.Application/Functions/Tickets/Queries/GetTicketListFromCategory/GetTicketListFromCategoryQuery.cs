using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList
{
    public class GetTicketListFromCategoryQuery : IRequest<List<TicketListViewModel>>
    {
        public int CategoryId { get; set; }
    }
}
