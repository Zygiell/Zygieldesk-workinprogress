﻿using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Commands
{
    public class CreateTicketCommentCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;
        private readonly Mock<ITicketRepository> _mockTicketRepository;

        public CreateTicketCommentCommandHandlerTests()
        {
            _mockTicketCommentRepository = RepositoryMocks.GetTicketCommentRepository();
            _mockTicketRepository = RepositoryMocks.GetTicketRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task AddTicketCommentHandlerTest()
        {
            var handler = new CreateTicketCommentCommandHandler(_mapper, _mockTicketCommentRepository.Object, _mockTicketRepository.Object);
            var allTicketCommentsBeforeCount = (await _mockTicketCommentRepository.Object.GetAllAsync()).Count;

            var response = await handler.Handle(new CreateTicketCommentCommand()
            {
                TicketId = 1,
                CommentBody = "TestBody"
            }, CancellationToken.None);
            var allTicketComments = await _mockTicketCommentRepository.Object.GetAllAsync();

            response.Success.ShouldBe(true);
            response.ValidationErrors.Count.ShouldBe(0);
            allTicketComments.Count.ShouldBe(allTicketCommentsBeforeCount + 1);
        }
    }
}