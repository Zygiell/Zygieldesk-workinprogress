using FluentValidation;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommandValidator : AbstractValidator<ChangeUserRoleCommand>
    {
        public ChangeUserRoleCommandValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty();
            RuleFor(r => r.RoleId)
                .NotEmpty();
        }
    }
}