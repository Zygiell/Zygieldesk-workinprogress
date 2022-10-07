using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Queries
{
    public class GetTicketCommentByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public GetTicketCommentByIdQueryHandlerTests()
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
        public async Task GetTicketCommentsByIdQueryTest()
        {
            var query = new GetTicketCommentByIdQuery() { TicketCommentId = 1 };
            var handler = new GetTicketCommentByIdQueryHandler(_mapper, _mockTicketCommentRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = handler.Handle(query, CancellationToken.None);

            query.TicketCommentId = 27;
            var result2 = handler.Handle(query, CancellationToken.None);

            result.Result.Id.ShouldBe(1);
            result.Result.ShouldBeOfType<TicketCommentViewModel>();
            result.Result.TicketId.ShouldBe(1);

            result2.Result.Id.ShouldBe(27);
            result2.Result.ShouldBeOfType<TicketCommentViewModel>();
            result2.Result.TicketId.ShouldBe(9);
        }
    }
}