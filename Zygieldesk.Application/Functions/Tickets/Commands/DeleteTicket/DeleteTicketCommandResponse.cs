using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandResponse : BaseResponse
    {
        public int TicketId { get; set; }
        public DeleteTicketCommandResponse()
        {
        }

        public DeleteTicketCommandResponse(string message = null) : base(message)
        {
        }

        public DeleteTicketCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public DeleteTicketCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public DeleteTicketCommandResponse(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
