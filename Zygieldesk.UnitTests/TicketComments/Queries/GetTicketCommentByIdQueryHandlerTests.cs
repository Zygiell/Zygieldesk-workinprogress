﻿using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.TicketComments.Queries
{
    public class GetTicketCommentByIdQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketCommentRepository> _mockTicketCommentRepository;

        public GetTicketCommentByIdQueryHandlerTests()
        {
            _mockTicketCommentRepository = RepositoryMocks.GetTicketCommentRepository();
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetTicketCommentsByIdQueryTest()
        {
            var query = new GetTicketCommentByIdQuery() { TicketCommentId = 1 };
            var handler = new GetTicketCommentByIdQueryHandler(_mapper, _mockTicketCommentRepository.Object);
            var result = handler.Handle(query, CancellationToken.None);

            query.TicketCommentId = 27;
            var result2 = handler.Handle(query, CancellationToken.None);

            result.Result.Id.ShouldBe(1);
            result.Result.ShouldBeOfType<TicketCommentViewModel>();
            result.Result.TicketId.ShouldBe(1);

            result2.Result.Id.ShouldBe(27);
            result2.Result.ShouldBeOfType<TicketCommentViewModel>();
            result2.Result.TicketId.ShouldBe(9);
        }
    }
}