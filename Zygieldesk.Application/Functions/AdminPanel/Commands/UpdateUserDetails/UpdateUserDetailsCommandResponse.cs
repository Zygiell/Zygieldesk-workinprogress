using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.AdminPanel.Commands.UpdateUserDetails
{
    public class UpdateUserDetailsCommandResponse : BaseResponse
    {
        public UpdateUserDetailsCommandResponse()
        {
        }

        public UpdateUserDetailsCommandResponse(string message = null) : base(message)
        {
        }

        public UpdateUserDetailsCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public UpdateUserDetailsCommandResponse(string message, bool success) : base(message, success)
        {
        }
    }
}
