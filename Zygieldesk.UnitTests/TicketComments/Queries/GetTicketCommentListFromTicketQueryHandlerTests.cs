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
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Queries
{
    public class GetTicketCommentListFromTicketQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public GetTicketCommentListFromTicketQueryHandlerTests()
        {
            _mockTicketCommentRepository = RepositoryMocks.GetTicketCommentRepository();
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
        public async Task GetTicketCommentListFromTicketQueryTest()
        {
            var query = new GetTicketCommentListFromTicketQuery() { TicketId = 1 };
            var handler = new GetTicketCommentListFromTicketQueryHandler(_mapper, _mockTicketCommentRepository.Object, _mockTicketRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            query.TicketId = 4;
            var result2 = await handler.Handle(query, CancellationToken.None);

            query.TicketId = 9;
            var result3 = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<List<TicketCommentListViewModel>>();
            result.Count.ShouldBe(3);
            foreach(var e in result)
            {
                e.TicketId.ShouldBe(1);
            }

            result2.ShouldBeOfType<List<TicketCommentListViewModel>>();
            result2.Count.ShouldBe(3);
            foreach (var e in result2)
            {
                e.TicketId.ShouldBe(4);
            }

            result3.ShouldBeOfType<List<TicketCommentListViewModel>>();
            result3.Count.ShouldBe(3);
            foreach (var e in result3)
            {
                e.TicketId.ShouldBe(9);
            }
        }
    }
}
