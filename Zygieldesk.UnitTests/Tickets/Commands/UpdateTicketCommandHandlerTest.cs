using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Commands
{
    public class UpdateTicketCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public UpdateTicketCommandHandlerTest()
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
        public async Task UpdateTicketTest()
        {
            var handler = new UpdateTicketCommandHandler(_mapper, _mockTicketRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(new UpdateTicketCommand { TicketBody = "New body", TicketTitle = "New name", TicketId = 1 }, CancellationToken.None);
            var ticketToUpdate = await _mockTicketRepository.Object.GetByIdAsync(1);

            result.ShouldBeOfType<UpdateTicketCommandResponse>();
            ticketToUpdate.TicketTitle.ShouldBe("New name");
        }
    }
}