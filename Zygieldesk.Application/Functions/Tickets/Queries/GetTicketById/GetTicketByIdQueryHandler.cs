using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetTicketByIdQueryHandler(IMapper mapper, ITicketRepository ticketRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<TicketViewModel> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticket == null)
            {
                throw new NotFoundException($"Ticket with {request.TicketId} id does not exist.");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticket,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            return _mapper.Map<TicketViewModel>(ticket);
        }
    }
}