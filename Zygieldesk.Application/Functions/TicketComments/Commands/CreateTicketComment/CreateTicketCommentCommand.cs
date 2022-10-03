using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment
{
    public class CreateTicketCommentCommand : IRequest<CreateTicketCommentCommandResponse>
    {
        public string CommentBody { get; set; }
        public int TicketId { get; set; }
    }
}
