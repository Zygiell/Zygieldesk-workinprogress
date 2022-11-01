using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Tickets.Commands.ChangeTicketPriority
{
    public class ChangeTicketPriorityCommand : IRequest<ChangeTicketPriorityCommandResponse>
    {
        public int TicketId { get; set; }
        public TicketPriority TicketPriority { get; set; }
    }
}
