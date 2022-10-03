using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Ticket.Queries.GetTicketById;

namespace Zygieldesk.Application.Functions.Ticket.Queries.GetAllTickets
{
    public class GetAllTicketsQuery : IRequest<List<TicketViewModel>>
    {

    }
}
