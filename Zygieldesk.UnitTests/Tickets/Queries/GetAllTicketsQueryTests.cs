using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Queries.GetAllTickets;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Queries
{
    public class GetAllTicketsQueryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public GetAllTicketsQueryTests()
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
        public async Task GetAllTicketsTests()
        {
            var handler = new GetAllTicketsQueryHandler(_mapper, _mockTicketRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(new GetAllTicketsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<TicketViewModel>>();
            result.Count.ShouldBe(9);
        }
    }
}