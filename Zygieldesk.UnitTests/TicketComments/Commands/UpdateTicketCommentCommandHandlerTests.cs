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
using Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Commands
{
    public class UpdateTicketCommentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public UpdateTicketCommentCommandHandlerTests()
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
        public async Task UpdateTicketCommentCommandTest()
        {
            var handler = new UpdateTicketCommentCommandHandler(_mapper, _mockTicketCommentRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(new UpdateTicketCommentCommand { CommentBody = "New body", TicketCommentId = 1 }, CancellationToken.None);
            var ticketCommentToUpdate = await _mockTicketCommentRepository.Object.GetByIdAsync(1);

            result.ShouldBeOfType<UpdateTicketCommentCommandResponse>();
            ticketCommentToUpdate.CommentBody.ShouldBe("New body");
        }
    }
}
