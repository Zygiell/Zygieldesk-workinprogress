using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Queries
{
    public class GetTicketListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public GetTicketListQueryHandlerTests()
        {
            _mockTicketRepository = RepositoryMocks.GetTicketRepository();
            _mockAuthorizationService = ServiceMocks.GetAuthorizationService();
            _mockUserContextService = ServiceMocks.GetUserContextService();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetTicketListForCategoryTests()
        {
            var query = new GetTicketListFromCategoryQuery();
            query.CategoryId = 1;
            var handler = new GetTicketListFromCategoryQueryHandler(_mapper, _mockTicketRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            query.CategoryId = 2;
            var result2 = await handler.Handle(query, CancellationToken.None);

            query.CategoryId = 3;
            var result3 = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<List<TicketListViewModel>>();
            result.Count.ShouldBe(3);
            foreach (var e in result)
            {
                e.CategoryId.ShouldBe(1);
            }

            result2.ShouldBeOfType<List<TicketListViewModel>>();
            result2.Count.ShouldBe(3);
            foreach (var e in result2)
            {
                e.CategoryId.ShouldBe(2);
            }

            result3.ShouldBeOfType<List<TicketListViewModel>>();
            result3.Count.ShouldBe(3);
            foreach (var e in result3)
            {
                e.CategoryId.ShouldBe(3);
            }
        }
    }
}