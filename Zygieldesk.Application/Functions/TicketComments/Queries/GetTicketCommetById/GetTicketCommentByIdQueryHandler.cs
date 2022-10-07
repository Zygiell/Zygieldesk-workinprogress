using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Exceptions;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById
{
    public class GetTicketCommentByIdQueryHandler : IRequestHandler<GetTicketCommentByIdQuery, TicketCommentViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public GetTicketCommentByIdQueryHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<TicketCommentViewModel> Handle(GetTicketCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var ticketComment = await _ticketCommentRepository.GetByIdAsync(request.TicketCommentId);
            if (ticketComment == null)
            {
                throw new NotFoundException($"Ticket comment with {request.TicketCommentId} id does not exist.");
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketComment,
                new ResourceOperationRequirement(ResourceOperation.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbiddenException("Forbidden");
            }

            return _mapper.Map<TicketCommentViewModel>(ticketComment);
        }
    }
}