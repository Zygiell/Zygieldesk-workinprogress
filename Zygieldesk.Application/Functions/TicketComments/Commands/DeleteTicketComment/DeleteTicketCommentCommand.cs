using MediatR;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment
{
    public class DeleteTicketCommentCommand : IRequest<DeleteTicketCommentCommandResponse>
    {
        public int TicketCommentId { get; set; }
    }
}