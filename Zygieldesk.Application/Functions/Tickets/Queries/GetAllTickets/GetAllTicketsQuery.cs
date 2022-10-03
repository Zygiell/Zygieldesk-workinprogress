using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetAllTickets
{
    public class GetAllTicketsQuery : IRequest<List<TicketViewModel>>
    {

    }
}
