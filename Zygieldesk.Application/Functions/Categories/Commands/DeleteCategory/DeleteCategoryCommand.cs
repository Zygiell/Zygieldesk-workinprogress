using MediatR;

namespace Zygieldesk.Application.Functions.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
    {
        public int CategoryId { get; set; }
    }
}