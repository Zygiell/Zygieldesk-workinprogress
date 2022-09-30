using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList
{
    public class GetTicketListQuery : IRequest<List<TicketListViewModel>>
    {
        public int CategoryId { get; set; }
    }
}
