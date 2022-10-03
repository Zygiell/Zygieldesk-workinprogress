using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandResponse : BaseResponse
    {
        public int TicketId { get; set; }
        public UpdateTicketCommandResponse()
        {
        }

        public UpdateTicketCommandResponse(string message = null) : base(message)
        {
        }

        public UpdateTicketCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public UpdateTicketCommandResponse(string message, bool success) : base(message, success)
        {
        }
        public UpdateTicketCommandResponse(int ticketId)
        {
            TicketId = ticketId;
        }
    }
}
