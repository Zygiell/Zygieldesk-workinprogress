using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Queries.GetAllTickets;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Queries
{
    public class GetAllTicketsQueryTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        public GetAllTicketsQueryTests()
        {
            _mockTicketRepository = RepositoryMocks.GetTicketRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetAllTicketsTests()
        {
            var handler = new GetAllTicketsQueryHandler(_mapper, _mockTicketRepository.Object);
            var result = await handler.Handle(new GetAllTicketsQuery(), CancellationToken.None);

            result.ShouldBeOfType<List<TicketViewModel>>();
            result.Count.ShouldBe(9);
        }
    }
}
