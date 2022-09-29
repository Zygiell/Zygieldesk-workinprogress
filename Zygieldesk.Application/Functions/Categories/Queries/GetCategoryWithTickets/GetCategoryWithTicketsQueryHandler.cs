using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;

namespace Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets
{
    public class GetCategoryWithTicketsQueryHandler : IRequestHandler<GetCategoryWithTicketsQuery, CategoryWithTitcketsViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryWithTicketsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryWithTitcketsViewModel> Handle(GetCategoryWithTicketsQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            return _mapper.Map<CategoryWithTitcketsViewModel>(category);
        }
    }
}
