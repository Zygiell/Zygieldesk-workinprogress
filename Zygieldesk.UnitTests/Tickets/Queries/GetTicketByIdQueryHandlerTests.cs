using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Queries
{
    public class GetTicketByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        public GetTicketByIdQueryHandlerTests()
        {
            _mockTicketRepository = RepositoryMocks.GetTicketRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetTicketByIdQueryTest()
        {
            var query = new GetTicketByIdQuery() { TicketId = 1 };
            var handler = new GetTicketByIdQueryHandler(_mapper, _mockTicketRepository.Object);
            var result = handler.Handle(query, CancellationToken.None);

            query.TicketId = 4;
            var result2 = handler.Handle(query, CancellationToken.None);

            result.Result.Id.ShouldBe(1);
            result.Result.ShouldBeOfType<TicketViewModel>();
            result.Result.CategoryId.ShouldBe(1);

            result2.Result.Id.ShouldBe(4);
            result2.Result.ShouldBeOfType<TicketViewModel>();
            result2.Result.CategoryId.ShouldBe(2);


        }
    }
}
