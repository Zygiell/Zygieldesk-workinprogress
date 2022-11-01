using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Tickets.Commands.ChangeTicketPriority
{
    public class ChangeTicketPriorityCommandHandler : IRequestHandler<ChangeTicketPriorityCommand, ChangeTicketPriorityCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public ChangeTicketPriorityCommandHandler(IMapper mapper, ITicketRepository ticketRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<ChangeTicketPriorityCommandResponse> Handle(ChangeTicketPriorityCommand request, CancellationToken cancellationToken)
        {
            if (request.TicketPriority != TicketPriority.Normal ||
                request.TicketPriority != TicketPriority.Urgent)
            {
                throw new BadRequestException("Ticket Priority value has to be between 1 - 2");
            }
            var ticketToChange = await _ticketRepository.GetByIdAsync(request.TicketId);
            if (ticketToChange == null)
            {
                throw new NotFoundException($"Ticket with {request.TicketId} id, does not exist");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketToChange,
                new ResourceOperationRequirement(ResourceOperation.SetPriority)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }
            ticketToChange.TicketPriority = request.TicketPriority;
            await _ticketRepository.UpdateAsync(ticketToChange);

            return new ChangeTicketPriorityCommandResponse("Ticket priority succesfully updated", true);
        }
    }
}
