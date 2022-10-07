using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Categories.Commands
{
    public class CreateCategoryCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public CreateCategoryCommandHandlerTest()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            _mockAuthorizationService = ServiceMocks.GetAuthorizationService();
            _mockUserContextService = ServiceMocks.GetUserContextService();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }

        [Fact]
        public async Task Handle_ValidCategory_AddedToCategoriesRepo()
        {
            var handler = new CreatedCategoryCommandHandler(_mapper, _mockCategoryRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var allCategoriesBeforeCount = (await _mockCategoryRepository.Object.GetAllAsync()).Count;

            var response = await handler.Handle(new CreatedCategoryCommand()
            {
                Name = "Test",
                Description = "Test"
            }, CancellationToken.None);

            var allCategories = await _mockCategoryRepository.Object.GetAllAsync();

            response.Success.ShouldBe(true);
            response.ValidationErrors.Count.ShouldBe(0);
            allCategories.Count.ShouldBe(allCategoriesBeforeCount + 1);
            response.CategoryId.ShouldNotBeNull();

            var response2 = await handler.Handle(new CreatedCategoryCommand()
            {
                Name = "Test2",
                Description = "Test2"
            }, CancellationToken.None);

            response2.Success.ShouldBe(true);
            response2.ValidationErrors.Count.ShouldBe(0);
            allCategories.Count.ShouldBe(allCategoriesBeforeCount + 2);
            response2.CategoryId.ShouldNotBeNull();
        }

    }
}
