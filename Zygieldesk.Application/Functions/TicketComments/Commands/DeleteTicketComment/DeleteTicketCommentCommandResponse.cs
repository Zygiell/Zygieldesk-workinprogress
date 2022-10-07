using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment
{
    public class DeleteTicketCommentCommandResponse : BaseResponse
    {
        public int TicketCommentId { get; set; }

        public DeleteTicketCommentCommandResponse()
        {
        }

        public DeleteTicketCommentCommandResponse(string message = null) : base(message)
        {
        }

        public DeleteTicketCommentCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public DeleteTicketCommentCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public DeleteTicketCommentCommandResponse(int ticketCommentId)
        {
            TicketCommentId = ticketCommentId;
        }

        public DeleteTicketCommentCommandResponse(ResponseStatus status, string message) : base(status, message)
        {
        }

        public DeleteTicketCommentCommandResponse(ResponseStatus status, string message, ValidationResult validationResult) : base(status, message, validationResult)
        {
        }
    }
}