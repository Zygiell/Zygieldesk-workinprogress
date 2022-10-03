using AutoMapper;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Queries
{
    public class GetTicketListQueryHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;

        public GetTicketListQueryHandlerTests()
        {
            _mockTicketRepository = RepositoryMocks.GetTicketRepository();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task GetTicketListForCategoryTests()
        {
            var query = new GetTicketListFromCategoryQuery();
            query.CategoryId = 1;
            var handler = new GetTicketListFromCategoryQueryHandler(_mapper, _mockTicketRepository.Object);
            var result = await handler.Handle(query, CancellationToken.None);

            query.CategoryId = 2;
            var result2 = await handler.Handle(query, CancellationToken.None);

            query.CategoryId = 3;
            var result3 = await handler.Handle(query, CancellationToken.None);

            result.ShouldBeOfType<List<TicketListViewModel>>();
            result.Count.ShouldBe(3);
            foreach(var e in result)
            {
                e.CategoryId.ShouldBe(1);
            }

            

            result2.ShouldBeOfType<List<TicketListViewModel>>();
            result2.Count.ShouldBe(3);
            foreach (var e in result2)
            {
                e.CategoryId.ShouldBe(2);
            }




            result3.ShouldBeOfType<List<TicketListViewModel>>();
            result3.Count.ShouldBe(3);
            foreach (var e in result3)
            {
                e.CategoryId.ShouldBe(3);
            }





        }
    }
}
