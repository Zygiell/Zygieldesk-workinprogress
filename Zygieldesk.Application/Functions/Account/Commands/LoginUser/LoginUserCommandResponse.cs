using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Account.Commands.LoginUser
{
    public class LoginUserCommandResponse : BaseResponse
    {
        public LoginUserCommandResponse()
        {
        }

        public LoginUserCommandResponse(string message = null) : base(message)
        {
        }

        public LoginUserCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public LoginUserCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}
