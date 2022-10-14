using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Account.Commands.UpdateUser
{
    public class UpdateUserCommandResponse : BaseResponse
    {
        public UpdateUserCommandResponse()
        {
        }

        public UpdateUserCommandResponse(string message = null) : base(message)
        {
        }

        public UpdateUserCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public UpdateUserCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}