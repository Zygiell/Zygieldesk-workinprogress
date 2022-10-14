using FluentValidation;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommandValidator : AbstractValidator<UpdateUserDetailsCommand>
    {
        public UpdateUserDetailsCommandValidator()
        {
            RuleFor(x => x.Password)
                .MinimumLength(8)
                .When(p => p.Password.Length > 0);

            RuleFor(x => x.ConfirmPassword)
                .Equal(e => e.Password);
        }
    }
}