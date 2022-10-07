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

namespace Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment
{
    public class CreateTicketCommentCommandHandler : IRequestHandler<CreateTicketCommentCommand, CreateTicketCommentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public CreateTicketCommentCommandHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository, ITicketRepository ticketRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
            _ticketRepository = ticketRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public async Task<CreateTicketCommentCommandResponse> Handle(CreateTicketCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateTicketCommentCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                return new CreateTicketCommentCommandResponse(validationResult);
            }

            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            if(ticket == null)
            {
                return new CreateTicketCommentCommandResponse(ResponseStatus.NotFound, $"Ticket id {request.TicketId} you are trying to comment does not exist",
                    validationResult);
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticket,
                new ResourceOperationRequirement(ResourceOperation.Reply)).Result;

            if (!authorizationResult.Succeeded)
            {
                return new CreateTicketCommentCommandResponse(ResponseStatus.Forbidden, "Forbidden", validationResult);
            }


            var ticketComment = _mapper.Map<TicketComment>(request);
            ticketComment = await _ticketCommentRepository.AddAsync(ticketComment);
            ticket.Status = TicketStatus.Open;
            await _ticketRepository.UpdateAsync(ticket);

            return new CreateTicketCommentCommandResponse(ticketComment.Id);
        }
    }
}
