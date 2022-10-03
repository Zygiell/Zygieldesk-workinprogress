using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment
{
    public class UpdateTicketCommentCommand : IRequest<UpdateTicketCommentCommandResponse>
    {
        public int TicketCommentId { get; set; }
        public string CommentBody { get; set; }
    }
}
