using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment
{
    public class CreateTicketCommentCommandValidator : AbstractValidator<CreateTicketCommentCommand>
    {
        public CreateTicketCommentCommandValidator()
        {
            RuleFor(tc=>tc.TicketId)
                .NotEmpty();
            RuleFor(tc => tc.CommentBody)
                .NotEmpty()
                .WithMessage("{PropertyName} cannot be empty.");
        }
    }
}
