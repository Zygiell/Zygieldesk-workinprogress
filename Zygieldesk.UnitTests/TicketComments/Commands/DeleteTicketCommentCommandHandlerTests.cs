using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Commands
{
    public class DeleteTicketCommentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;

        public DeleteTicketCommentCommandHandlerTests()
        {
            _mockTicketCommentRepository = RepositoryMocks.GetTicketCommentRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task TicketCommentDeleteCommandTest()
        {
            var allTicketComments = await _mockTicketCommentRepository.Object.GetAllAsync();
            var handler = new DeleteTicketCommentCommandHandler(_mapper, _mockTicketCommentRepository.Object);
            var result = await handler.Handle(new DeleteTicketCommentCommand { TicketCommentId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<DeleteTicketCommentCommandResponse>();
            allTicketComments.Count.ShouldBe(26);
        }
    }
}
