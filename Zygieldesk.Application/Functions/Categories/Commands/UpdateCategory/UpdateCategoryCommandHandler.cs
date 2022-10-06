using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository, IAuthorizationService authorizationService,
            IUserContextService userContextService)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }


        public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCategoryCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new UpdateCategoryCommandResponse(validatorResult);
            }

            var categoryToUpdate = await _categoryRepository.GetByIdAsync(request.Id);

            if (categoryToUpdate == null)
            {
                return new UpdateCategoryCommandResponse($"Category with {request.Id} id does not exist", false);
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, categoryToUpdate, 
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return new UpdateCategoryCommandResponse("Forbidden");
            }

            categoryToUpdate.Name = request.Name;
            categoryToUpdate.Description = request.Description;            
            
            await _categoryRepository.UpdateAsync(categoryToUpdate);

            return new UpdateCategoryCommandResponse("Category successfully updated", true);


        }
    }
}
