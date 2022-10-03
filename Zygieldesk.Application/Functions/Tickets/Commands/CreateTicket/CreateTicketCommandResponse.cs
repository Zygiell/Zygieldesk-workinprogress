using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandResponse : BaseResponse
    {
        public int TicketId { get; set; }
        public CreateTicketCommandResponse()
        {
        }

        public CreateTicketCommandResponse(string message = null) : base(message)
        {
        }

        public CreateTicketCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateTicketCommandResponse(string message, bool success) : base(message, success)
        {
        }
        public CreateTicketCommandResponse(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
