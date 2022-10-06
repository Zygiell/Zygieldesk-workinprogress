using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

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
            var category = await _categoryRepository.GetCategoryWithTickets(request.CategoryId);
            if(category == null)
            {
                return null;
            }

            var mappedTicketsList = new List<CategoryTicketDto>();

            foreach(var ticket in category.Tickets)            
            {
                
                mappedTicketsList.Add(_mapper.Map<CategoryTicketDto>(ticket));
            }
            var mappedCategory = _mapper.Map<CategoryWithTitcketsViewModel>(category);
            mappedCategory.Tickets = mappedTicketsList;

            return mappedCategory;
        }

    }
}
