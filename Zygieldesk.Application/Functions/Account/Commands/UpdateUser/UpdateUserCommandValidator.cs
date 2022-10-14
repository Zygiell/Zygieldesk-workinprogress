using FluentValidation;

namespace Zygieldesk.Application.Functions.Account.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .When(p => p.Password.Length > 0);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);
        }
    }
}