using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Queries
{
    public class GetAllTicketCommentsQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;

        public GetAllTicketCommentsQueryHandlerTests()
        {
            _mockTicketCommentRepository = RepositoryMocks.GetTicketCommentRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }
        [Fact]
        public async Task GetAllTicketComentsTests()
        {
            var handler = new GetAllTicketsCommentsQueryHandler(_mapper, _mockTicketCommentRepository.Object);
            var result = await handler.Handle(new GetAllTicketsCommentsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<TicketCommentViewModel>>();
            result.Count.ShouldBe(27);
        }
    }
}
