using AutoMapper;
using MediatR;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, CreateTicketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly ICategoryRepository _categoryRepository;

        public CreateTicketCommandHandler(IMapper mapper, ITicketRepository ticketRepository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<CreateTicketCommandResponse> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                return new CreateTicketCommandResponse($"Category you are trying to add ticket to (Category id: {request.CategoryId}) does not exist.", false);
            }

            var ticket = _mapper.Map<Ticket>(request);
            ticket.Status = TicketStatus.Open;

            ticket = await _ticketRepository.AddAsync(ticket);

            return new CreateTicketCommandResponse(ticket.Id);
        }
    }
}