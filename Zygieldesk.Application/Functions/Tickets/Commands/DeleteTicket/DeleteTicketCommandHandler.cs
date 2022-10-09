using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Functions.Responses;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, DeleteTicketCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public DeleteTicketCommandHandler(IMapper mapper, ITicketRepository ticketRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<DeleteTicketCommandResponse> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToDelete = await _ticketRepository.GetByIdAsync(request.TicketId);

            if (ticketToDelete == null)
            {
                throw new NotFoundException($"Ticket with {request.TicketId} id, does not exist");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketToDelete,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            await _ticketRepository.DeleteAsync(ticketToDelete);

            return new DeleteTicketCommandResponse();
        }
    }
}