using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Responses;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Categories.Commands.CreateCategory
{
    public class CreatedCategoryCommandHandler : IRequestHandler<CreatedCategoryCommand, CreatedCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public CreatedCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<CreatedCategoryCommandResponse> Handle(CreatedCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, category,
                new ResourceOperationRequirement(ResourceOperation.Create)).Result;

            if (!authorizationResult.Succeeded)
            {
                return new CreatedCategoryCommandResponse(ResponseStatus.Forbidden, "Forbidden");
            }

            category = await _categoryRepository.AddAsync(category);

            return new CreatedCategoryCommandResponse(category.Id);
        }
    }
}