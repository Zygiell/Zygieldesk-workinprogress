using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment
{
    public class CreateTicketCommentCommandResponse : BaseResponse
    {
        public int TicketCommentId { get; set; }

        public CreateTicketCommentCommandResponse()
        {
        }

        public CreateTicketCommentCommandResponse(string message = null) : base(message)
        {
        }

        public CreateTicketCommentCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateTicketCommentCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public CreateTicketCommentCommandResponse(int ticketCommentId)
        {
            TicketCommentId = ticketCommentId;
        }

        public CreateTicketCommentCommandResponse(ResponseStatus status, string message) : base(status, message)
        {
        }

        public CreateTicketCommentCommandResponse(ResponseStatus status, string message, ValidationResult validationResult) : base(status, message, validationResult)
        {
        }
    }
}