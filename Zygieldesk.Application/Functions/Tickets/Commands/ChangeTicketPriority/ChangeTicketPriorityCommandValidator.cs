using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Tickets.Commands.ChangeTicketPriority
{
    public class ChangeTicketPriorityCommandValidator : AbstractValidator<ChangeTicketPriorityCommand>
    {
        public ChangeTicketPriorityCommandValidator()
        {
            RuleFor(x => x.TicketPriority)
                .NotEmpty();
            RuleFor(x => x.TicketId)
                .NotEmpty();
        }
    }
}
