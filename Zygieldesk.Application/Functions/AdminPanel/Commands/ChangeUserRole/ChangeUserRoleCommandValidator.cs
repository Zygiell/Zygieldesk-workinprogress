using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
