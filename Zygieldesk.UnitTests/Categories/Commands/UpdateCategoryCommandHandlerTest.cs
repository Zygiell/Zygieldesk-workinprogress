using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Categories.Commands.UpdateCategory;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Categories.Commands
{
    public class UpdateCategoryCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public UpdateCategoryCommandHandlerTest()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }


        [Fact]
        public async Task UpdateCategoryTest()
        {
            var handler = new UpdateCategoryCommandHandler(_mapper, _mockCategoryRepository.Object);
            var result = await handler.Handle(new UpdateCategoryCommand { Name = "New name", Id = 1 }, CancellationToken.None);
            var categoryToUpdate = await _mockCategoryRepository.Object.GetByIdAsync(1);



            result.ShouldBeOfType<UpdateCategoryCommandResponse>();
            categoryToUpdate.Name.ShouldBe("New name");
            
        }
    }
}
