using MediatR;

namespace Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets
{
    public class GetCategoryWithTicketsQuery : IRequest<CategoryWithTitcketsViewModel>
    {
        public int CategoryId { get; set; }
    }
}