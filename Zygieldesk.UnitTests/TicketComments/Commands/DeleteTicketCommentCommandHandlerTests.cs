﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment;
using Zygieldesk.Application.Mapper;
using Zygieldesk.Application.Services;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Commands
{
    public class DeleteTicketCommentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;
        private readonly Mock<IAuthorizationService> _mockAuthorizationService;
        private readonly Mock<IUserContextService> _mockUserContextService;

        public DeleteTicketCommentCommandHandlerTests()
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
        public async Task TicketCommentDeleteCommandTest()
        {
            var allTicketComments = await _mockTicketCommentRepository.Object.GetAllAsync();
            var handler = new DeleteTicketCommentCommandHandler(_mapper, _mockTicketCommentRepository.Object,
                _mockAuthorizationService.Object, _mockUserContextService.Object);
            var result = await handler.Handle(new DeleteTicketCommentCommand { TicketCommentId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<DeleteTicketCommentCommandResponse>();
            allTicketComments.Count.ShouldBe(26);
        }
    }
}