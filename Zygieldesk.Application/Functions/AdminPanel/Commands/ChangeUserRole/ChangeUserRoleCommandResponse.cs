using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.ChangeUserRole
{
    public class ChangeUserRoleCommandResponse : BaseResponse
    {
        public ChangeUserRoleCommandResponse()
        {
        }

        public ChangeUserRoleCommandResponse(string message = null) : base(message)
        {
        }

        public ChangeUserRoleCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public ChangeUserRoleCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}