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
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Services;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList
{
    public class GetTicketListFromCategoryQueryHandler : IRequestHandler<GetTicketListFromCategoryQuery, List<TicketListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetTicketListFromCategoryQueryHandler(IMapper mapper, ITicketRepository ticketRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<List<TicketListViewModel>> Handle(GetTicketListFromCategoryQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAllTicketsFromCategoryAsync(request.CategoryId);
            if(tickets == null)
            {
                throw new NotFoundException($"Category with {request.CategoryId} id does not exist.");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, tickets,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            return _mapper.Map<List<TicketListViewModel>>(tickets);
        }
    }
}
