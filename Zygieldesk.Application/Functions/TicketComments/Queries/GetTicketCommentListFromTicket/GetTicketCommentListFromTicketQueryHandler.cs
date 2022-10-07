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
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList
{
    public class GetTicketCommentListFromTicketQueryHandler : IRequestHandler<GetTicketCommentListFromTicketQuery, List<TicketCommentListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetTicketCommentListFromTicketQueryHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository, ITicketRepository ticketRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
            _ticketRepository = ticketRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public async Task<List<TicketCommentListViewModel>> Handle(GetTicketCommentListFromTicketQuery request, CancellationToken cancellationToken)
        {
            var ticketComments = await _ticketCommentRepository.GetAllTicketCommentsFromTicketAsync(request.TicketId);
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);

            if (ticketComments== null)
            {
                throw new NotFoundException($"Ticket with {request.TicketId} id does not exist.");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticket,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            return _mapper.Map<List<TicketCommentListViewModel>>(ticketComments);
        }
    }
}
