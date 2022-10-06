using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Account.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(c=>c.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(c => c.Password)
                .NotEmpty();
        }
    }
}
