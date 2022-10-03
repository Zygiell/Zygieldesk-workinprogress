using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Ticket.Queries.GetTicketById
{
    public class GetTicketByIdQuery : IRequest<TicketViewModel>
    {
        public int TicketId { get; set; }
    }
}
