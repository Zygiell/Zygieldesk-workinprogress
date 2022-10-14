using FluentValidation.Results;
using Zygieldesk.Application.Functions.Responses;

namespace Zygieldesk.Application.Functions.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandResponse : BaseResponse
    {
        public int? CategoryId { get; set; }

        public DeleteCategoryCommandResponse()
        {
        }

        public DeleteCategoryCommandResponse(string message = null) : base(message)
        {
        }

        public DeleteCategoryCommandResponse(ValidationResult validationResult) : base(validationResult)
        {
        }

        public DeleteCategoryCommandResponse(string message, bool success) : base(message, success)
        {
        }

        public DeleteCategoryCommandResponse(int? categoryId)
        {
            CategoryId = categoryId;
        }
    }
}