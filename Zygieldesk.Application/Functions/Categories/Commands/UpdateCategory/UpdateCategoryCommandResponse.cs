using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandResponse : BaseResponse
    {
        public int? CategoryId { get; set; }

        public UpdateCategoryCommandResponse()
        {
        }

        public UpdateCategoryCommandResponse(string message = null) : base(message)
        {
        }

        public UpdateCategoryCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public UpdateCategoryCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public UpdateCategoryCommandResponse(int categoryId)
        {
            CategoryId = categoryId;
        }

        public UpdateCategoryCommandResponse(ResponseStatus status, string message, ValidationResult validationResult) : base(status, message, validationResult)
        {
        }

        public UpdateCategoryCommandResponse(ResponseStatus status, string message) : base(status, message)
        {
        }
    }
}