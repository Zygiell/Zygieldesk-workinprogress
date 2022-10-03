using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment
{
    public class UpdateTicketCommentCommandResponse : BaseResponse
    {
        public int TicketCommentId { get; set; }
        public UpdateTicketCommentCommandResponse()
        {
        }

        public UpdateTicketCommentCommandResponse(string message = null) : base(message)
        {
        }

        public UpdateTicketCommentCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public UpdateTicketCommentCommandResponse(string message, bool success) : base(message, success)
        {
        }
        public UpdateTicketCommentCommandResponse(int ticketCommentId)
        {
            TicketCommentId = ticketCommentId;  
        }
    }
}
