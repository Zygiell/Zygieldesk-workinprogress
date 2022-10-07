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
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketComments
{
    public class GetAllTicketsCommentsQueryHandler : IRequestHandler<GetAllTicketsCommentsQuery, List<TicketCommentViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetAllTicketsCommentsQueryHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public async Task<List<TicketCommentViewModel>> Handle(GetAllTicketsCommentsQuery request, CancellationToken cancellationToken)
        {
            var ticketCommentsList = await _ticketCommentRepository.GetAllAsync();
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketCommentsList,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            return _mapper.Map<List<TicketCommentViewModel>>(ticketCommentsList);
        }
    }
}
