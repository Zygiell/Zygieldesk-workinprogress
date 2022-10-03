using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
