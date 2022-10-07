using FluentValidation;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment
{
    public class UpdateTicketCommentCommandValidator : AbstractValidator<UpdateTicketCommentCommand>
    {
        public UpdateTicketCommentCommandValidator()
        {
            RuleFor(tc => tc.CommentBody)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty");
        }
    }
}