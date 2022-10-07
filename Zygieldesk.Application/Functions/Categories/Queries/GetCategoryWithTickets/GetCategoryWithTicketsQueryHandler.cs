using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets
{
    public class GetCategoryWithTicketsQueryHandler : IRequestHandler<GetCategoryWithTicketsQuery, CategoryWithTitcketsViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetCategoryWithTicketsQueryHandler(IMapper mapper, ICategoryRepository categoryRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<CategoryWithTitcketsViewModel> Handle(GetCategoryWithTicketsQuery request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryWithTickets(request.CategoryId);
            if (category == null)
            {
                throw new NotFoundException($"Category with {request.CategoryId} id does not exist");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, category,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            var mappedTicketsList = new List<CategoryTicketDto>();

            foreach (var ticket in category.Tickets)
            {
                mappedTicketsList.Add(_mapper.Map<CategoryTicketDto>(ticket));
            }
            var mappedCategory = _mapper.Map<CategoryWithTitcketsViewModel>(category);
            mappedCategory.Tickets = mappedTicketsList;

            return mappedCategory;
        }
    }
}