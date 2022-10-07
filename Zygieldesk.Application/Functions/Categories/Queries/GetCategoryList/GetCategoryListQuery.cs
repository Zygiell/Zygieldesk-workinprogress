using MediatR;

namespace Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<List<CategoryListViewModel>>
    {
    }
}