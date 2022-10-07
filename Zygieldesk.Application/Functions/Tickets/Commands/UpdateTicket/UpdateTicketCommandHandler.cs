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
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Functions.Responses;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

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
            var validator = new UpdateTicketCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new UpdateTicketCommandResponse(validatorResult);
            }

            var ticketToUpdate = await _ticketRepository.GetByIdAsync(request.TicketId);
            if(ticketToUpdate == null)
            {
                return new UpdateTicketCommandResponse(ResponseStatus.NotFound, $"Ticket with {request.TicketId} id, does not exist", validatorResult);
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketToUpdate,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return new UpdateTicketCommandResponse(ResponseStatus.Forbidden, "Forbidden", validatorResult);
            }
            ticketToUpdate.TicketTitle = request.TicketTitle;
            ticketToUpdate.TicketBody = request.TicketBody;

            await _ticketRepository.UpdateAsync(ticketToUpdate);

            return new UpdateTicketCommandResponse("Ticket successfully updated", true);


        }
    }
}
