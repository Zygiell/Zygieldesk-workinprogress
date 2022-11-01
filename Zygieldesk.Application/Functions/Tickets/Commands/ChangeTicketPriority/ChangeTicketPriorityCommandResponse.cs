using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Tickets.Commands.ChangeTicketPriority
{
    public class ChangeTicketPriorityCommandResponse : BaseResponse
    {
        public ChangeTicketPriorityCommandResponse()
        {
        }

        public ChangeTicketPriorityCommandResponse(string message = null) : base(message)
        {
        }

        public ChangeTicketPriorityCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public ChangeTicketPriorityCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}
