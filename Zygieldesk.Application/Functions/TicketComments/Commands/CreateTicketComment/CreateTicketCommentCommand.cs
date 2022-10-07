using MediatR;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment
{
    public class CreateTicketCommentCommand : IRequest<CreateTicketCommentCommandResponse>
    {
        public string CommentBody { get; set; }
        public int TicketId { get; set; }
    }
}