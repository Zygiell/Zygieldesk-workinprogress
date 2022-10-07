using FluentValidation;

namespace Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(t => t.CategoryId)
                .NotEmpty();

            RuleFor(t => t.TicketTitle)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(300)
                .WithMessage("{PropertyName} Length is between 2 and 300");

            RuleFor(t => t.TicketBody)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");
        }
    }
}