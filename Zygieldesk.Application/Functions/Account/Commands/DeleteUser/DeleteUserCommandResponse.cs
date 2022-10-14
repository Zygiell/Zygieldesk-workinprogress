using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Account.Commands.DeleteUser
{
    public class DeleteUserCommandResponse : BaseResponse
    {
        public DeleteUserCommandResponse()
        {
        }

        public DeleteUserCommandResponse(string message = null) : base(message)
        {
        }

        public DeleteUserCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public DeleteUserCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}