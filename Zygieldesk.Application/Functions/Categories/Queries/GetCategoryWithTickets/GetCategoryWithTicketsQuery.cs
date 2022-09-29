using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets
{
    public class GetCategoryWithTicketsQuery : IRequest<CategoryWithTitcketsViewModel>
    {
        public int CategoryId { get; set; }
    }
}
