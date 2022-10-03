using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommand : IRequest<CreateTicketCommandResponse>
    {
        public string TicketTitle { get; set; }
        public string TicketBody { get; set; }
        public int CategoryId { get; set; }
    }
}
