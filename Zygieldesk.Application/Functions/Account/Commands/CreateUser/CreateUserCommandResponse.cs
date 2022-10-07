using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Account.Commands.AddUser
{
    public class CreateUserCommandResponse : BaseResponse
    {
        public int UserId { get; set; }

        public CreateUserCommandResponse()
        {
        }

        public CreateUserCommandResponse(string message = null) : base(message)
        {
        }

        public CreateUserCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreateUserCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public CreateUserCommandResponse(int userId)
        {
            UserId = userId;
        }
    }
}