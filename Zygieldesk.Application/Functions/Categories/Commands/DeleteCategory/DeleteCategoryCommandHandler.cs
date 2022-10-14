using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public DeleteCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryToDelete = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (categoryToDelete == null)
            {
                throw new NotFoundException($"Category with {request.CategoryId} id, do not exist");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, categoryToDelete,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            await _categoryRepository.DeleteAsync(categoryToDelete);

            return new DeleteCategoryCommandResponse();
        }
    }
}