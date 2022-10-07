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
using Zygieldesk.Application.Functions.Tickets.Commands.DeleteTicket;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Commands
{
    public class DeleteTicketCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;
        public DeleteTicketCommandHandlerTest()
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
        public async Task TicketDeleteCommadTest()
        {
            var allTickets = await _mockTicketRepository.Object.GetAllAsync();

            var handler = new DeleteTicketCommandHandler(_mapper, _mockTicketRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(new DeleteTicketCommand { TicketId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<DeleteTicketCommandResponse>();
            allTickets.Count.ShouldBe(8);
        }
    }
}
