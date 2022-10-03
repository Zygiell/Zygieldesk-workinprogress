using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var validator = new CreateTicketCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateTicketCommandResponse(validationResult);
            }

            var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

            if (category == null)
            {
                return new CreateTicketCommandResponse($"Category you are trying to add ticket to (Category id: {request.CategoryId}) does not exist.", false);
            }

            var ticket = _mapper.Map<Ticket>(request);

            ticket = await _ticketRepository.AddAsync(ticket);

            return new CreateTicketCommandResponse(ticket.Id);
            
        }
    }
}
