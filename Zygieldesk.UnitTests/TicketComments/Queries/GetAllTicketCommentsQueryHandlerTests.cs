using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Queries
{
    public class GetAllTicketCommentsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public GetAllTicketCommentsQueryHandlerTests()
        {
            _mockTicketCommentRepository = RepositoryMocks.GetTicketCommentRepository();
            _mockAuthorizationService = ServiceMocks.GetAuthorizationService();
            _mockUserContextService = ServiceMocks.GetUserContextService();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAllTicketComentsTests()
        {
            var handler = new GetAllTicketsCommentsQueryHandler(_mapper, _mockTicketCommentRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(new GetAllTicketsCommentsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<TicketCommentViewModel>>();
            result.Count.ShouldBe(27);
        }
    }
}