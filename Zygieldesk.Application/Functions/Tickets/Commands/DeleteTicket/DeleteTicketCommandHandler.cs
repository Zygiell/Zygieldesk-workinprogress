using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;

namespace Zygieldesk.Application.Functions.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, DeleteTicketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;

        public DeleteTicketCommandHandler(IMapper mapper, ITicketRepository ticketRepository)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }
        public async Task<DeleteTicketCommandResponse> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToDelete = await _ticketRepository.GetByIdAsync(request.TicketId);

            if(ticketToDelete == null)
            {
                return new DeleteTicketCommandResponse($"Ticket with {request.TicketId} id, does not exist", false);
            }

            await _ticketRepository.DeleteAsync(ticketToDelete);

            return new DeleteTicketCommandResponse();
        }
    }
}
