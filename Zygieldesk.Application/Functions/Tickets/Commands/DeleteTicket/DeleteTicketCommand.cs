using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommand : IRequest<DeleteTicketCommandResponse>
    {
        public int TicketId { get; set; }
    }
}
