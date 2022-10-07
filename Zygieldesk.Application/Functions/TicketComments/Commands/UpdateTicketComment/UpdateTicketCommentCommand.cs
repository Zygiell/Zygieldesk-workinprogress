using MediatR;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment
{
    public class UpdateTicketCommentCommand : IRequest<UpdateTicketCommentCommandResponse>
    {
        public int TicketCommentId { get; set; }
        public string CommentBody { get; set; }
    }
}