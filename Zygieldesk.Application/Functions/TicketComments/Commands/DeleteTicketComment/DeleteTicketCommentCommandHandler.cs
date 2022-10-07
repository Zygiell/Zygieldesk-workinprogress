using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Zygieldesk.Application.Authorization;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Responses;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment
{
    public class DeleteTicketCommentCommandHandler : IRequestHandler<DeleteTicketCommentCommand, DeleteTicketCommentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public DeleteTicketCommentCommandHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public async Task<DeleteTicketCommentCommandResponse> Handle(DeleteTicketCommentCommand request, CancellationToken cancellationToken)
        {
            var ticketCommentToDelete = await _ticketCommentRepository.GetByIdAsync(request.TicketCommentId);

            if (ticketCommentToDelete == null)
            {
                return new DeleteTicketCommentCommandResponse($"Ticket with {request.TicketCommentId} id, does not exist", false);
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketCommentToDelete,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                return new DeleteTicketCommentCommandResponse(ResponseStatus.Forbidden, "Forbidden");
            }

            await _ticketCommentRepository.DeleteAsync(ticketCommentToDelete);

            return new DeleteTicketCommentCommandResponse();
        }
    }
}