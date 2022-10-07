using FluentValidation;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment
{
    public class CreateTicketCommentCommandValidator : AbstractValidator<CreateTicketCommentCommand>
    {
        public CreateTicketCommentCommandValidator()
        {
            RuleFor(tc => tc.TicketId)
                .NotEmpty();
            RuleFor(tc => tc.CommentBody)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}