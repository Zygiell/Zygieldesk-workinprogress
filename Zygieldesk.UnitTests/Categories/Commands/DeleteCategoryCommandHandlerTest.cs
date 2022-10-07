using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Categories.Commands.DeleteCategory;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Categories.Commands
{
    public class DeleteCategoryCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public DeleteCategoryCommandHandlerTest()
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
        public async Task Handle_Category_Delete()
        {
            var allCategories = await _mockCategoryRepository.Object.GetAllAsync();

            var handler = new DeleteCategoryCommandHandler(_mapper, _mockCategoryRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(new DeleteCategoryCommand { CategoryId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<DeleteCategoryCommandResponse>();
            allCategories.Count.ShouldBe(2);
        }
    }
}