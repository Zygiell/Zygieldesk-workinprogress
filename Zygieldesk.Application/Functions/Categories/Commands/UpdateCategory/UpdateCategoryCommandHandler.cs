using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
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
            categoryToUpdate.Name = request.Name;
            categoryToUpdate.Description = request.Description;            
            
            await _categoryRepository.UpdateAsync(categoryToUpdate);

            return new UpdateCategoryCommandResponse("Category successfully updated", true);


        }
    }
}
