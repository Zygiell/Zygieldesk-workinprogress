using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, UpdateTicketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public UpdateTicketCommandHandler(IMapper mapper, ITicketRepository ticketRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<UpdateTicketCommandResponse> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToUpdate = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticketToUpdate == null)
            {
                throw new NotFoundException($"Ticket with {request.TicketId} id, does not exist");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketToUpdate,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }
            ticketToUpdate.TicketTitle = request.TicketTitle;
            ticketToUpdate.TicketBody = request.TicketBody;

            await _ticketRepository.UpdateAsync(ticketToUpdate);

            return new UpdateTicketCommandResponse("Ticket successfully updated", true);
        }
    }
}