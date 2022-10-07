using AutoMapper;
using Moq;
using Shouldly;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket;
using Zygieldesk.Application.Mapper;
using Zygieldesk.UnitTests.Mocks;

namespace Zygieldesk.UnitTests.Tickets.Commands
{
    public class CreateTicketCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ITicketRepository> _mockTicketRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;

        public CreateTicketCommandHandlerTest()
        {
            _mockTicketRepository = RepositoryMocks.GetTicketRepository();
            _mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public async Task AddTicketCommandHandlerTest()
        {
            var handler = new CreateTicketCommandHandler(_mapper, _mockTicketRepository.Object, _mockCategoryRepository.Object);
            var allTicketsBeforeCount = (await _mockTicketRepository.Object.GetAllAsync()).Count;

            var response = await handler.Handle(new CreateTicketCommand()
            {
                CategoryId = 1,
                TicketTitle = "TestTitle",
                TicketBody = "TestBody"
            }, CancellationToken.None);

            var allTickets = await _mockTicketRepository.Object.GetAllAsync();

            response.Success.ShouldBe(true);
            response.ValidationErrors.Count.ShouldBe(0);
            allTickets.Count.ShouldBe(allTicketsBeforeCount + 1);
        }
    }
}