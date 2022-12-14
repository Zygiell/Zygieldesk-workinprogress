using FluentValidation;

namespace Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandValidator : AbstractValidator<UpdateTicketCommand>
    {
        public UpdateTicketCommandValidator()
        {
            RuleFor(t => t.TicketTitle)
                .MinimumLength(2)
                .MaximumLength(300)
                .WithMessage("{PropertyName} Length is between 2 and 300");
            RuleFor(t => t.TicketBody)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");
        }
    }
}