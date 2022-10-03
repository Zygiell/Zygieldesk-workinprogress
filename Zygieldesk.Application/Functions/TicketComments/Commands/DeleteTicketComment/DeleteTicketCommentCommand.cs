using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment
{
    public class DeleteTicketCommentCommand : IRequest<DeleteTicketCommentCommandResponse>
    {
        public int TicketCommentId { get; set; }
    }
}
