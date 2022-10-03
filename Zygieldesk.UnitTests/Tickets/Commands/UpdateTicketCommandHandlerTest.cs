using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Commands
{
    public class UpdateTicketCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        public UpdateTicketCommandHandlerTest()
        {
            _mockTicketRepository = RepositoryMocks.GetTicketRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task UpdateTicketTest()
        {
            var handler = new UpdateTicketCommandHandler(_mapper, _mockTicketRepository.Object);
            var result = await handler.Handle(new UpdateTicketCommand { TicketBody = "New body", TicketTitle = "New name", TicketId = 1 }, CancellationToken.None);
            var ticketToUpdate = await _mockTicketRepository.Object.GetByIdAsync(1);

            result.ShouldBeOfType<UpdateTicketCommandResponse>();
            ticketToUpdate.TicketTitle.ShouldBe("New name");

        }
    }
}
