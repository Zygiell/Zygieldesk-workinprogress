using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;

namespace Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, UpdateTicketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;

        public UpdateTicketCommandHandler(IMapper mapper, ITicketRepository ticketRepository)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }
        public async Task<UpdateTicketCommandResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTicketCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new UpdateTicketCommandResponse(validatorResult);
            }

            var ticketToUpdate = await _ticketRepository.GetByIdAsync(request.TicketId);
            if(ticketToUpdate == null)
            {
                return new UpdateTicketCommandResponse($"Ticket with {request.TicketId} id, does not exist", false);
            }
            ticketToUpdate.TicketTitle = request.TicketTitle;
            ticketToUpdate.TicketBody = request.TicketBody;

            await _ticketRepository.UpdateAsync(ticketToUpdate);

            return new UpdateTicketCommandResponse("Ticket successfully updated", true);


        }
    }
}
