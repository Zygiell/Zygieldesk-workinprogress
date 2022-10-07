using MediatR;

namespace Zygieldesk.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreatedCategoryCommand : IRequest<CreatedCategoryCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}