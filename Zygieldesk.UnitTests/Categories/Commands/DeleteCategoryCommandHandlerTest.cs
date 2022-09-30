using AutoMapper;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Functions.Categories.Commands.DeleteCategory;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Domain.Entities;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Categories.Commands
{
    public class DeleteCategoryCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public DeleteCategoryCommandHandlerTest()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_Category_Delete()
        {
            var allCategories = await _mockCategoryRepository.Object.GetAllAsync();

            var handler = new DeleteCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);
            var result = await handler.Handle(new DeleteCategoryCommand { CategoryId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<DeleteCategoryCommandResponse>();
            allCategories.Count.ShouldBe(2);


        }

    }
}
