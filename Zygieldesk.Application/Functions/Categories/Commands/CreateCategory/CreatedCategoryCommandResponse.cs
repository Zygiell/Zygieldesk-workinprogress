using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreatedCategoryCommandResponse : BaseResponse
    {
        public int? CategoryId { get; set; }

        public CreatedCategoryCommandResponse()
        {
        }

        public CreatedCategoryCommandResponse(string message = null) : base(message)
        {
        }

        public CreatedCategoryCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public CreatedCategoryCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public CreatedCategoryCommandResponse(int categoryId)
        {
            CategoryId = categoryId;
        }

        public CreatedCategoryCommandResponse(ResponseStatus status, string message, ValidationResult validationResult) : base(status, message, validationResult)
        {
        }

        public CreatedCategoryCommandResponse(ResponseStatus status, string message) : base(status, message)
        {
        }
    }
}