using FluentValidation;

namespace Zygieldesk.Application.Functions.Account.Commands.AddUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(e => e.Password)
                .MinimumLength(8)
                .NotEmpty();
            RuleFor(e => e.ConfirmPassword)
                .Equal(e => e.Password);
        }
    }
}