using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Domain.Entities;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Categories.Queries
{
    public class GetCategoryWithTicketsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public GetCategoryWithTicketsQueryHandlerTests()
        {
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetCategoriesListTest()
        {
            var query = new GetCategoryWithTicketsQuery();
            query.CategoryId = 1;
            var handler = new GetCategoryWithTicketsQueryHandler(_mapper, _mockCategoryRepository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            query.CategoryId = 2;
            var result2 = await handler.Handle(query, CancellationToken.None);

            query.CategoryId = 3;
            var result3 = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<CategoryWithTitcketsViewModel>();
            result.Tickets.Count.ShouldBe(3);
            result.Tickets.ShouldBeOfType<List<CategoryTicketDto>>();

            result2.ShouldBeOfType<CategoryWithTitcketsViewModel>();
            result2.Tickets.Count.ShouldBe(3);
            result2.Tickets.ShouldBeOfType<List<CategoryTicketDto>>();

            result3.ShouldBeOfType<CategoryWithTitcketsViewModel>();
            result3.Tickets.Count.ShouldBe(3);
            result3.Tickets.ShouldBeOfType<List<CategoryTicketDto>>();


        }
    }
}
