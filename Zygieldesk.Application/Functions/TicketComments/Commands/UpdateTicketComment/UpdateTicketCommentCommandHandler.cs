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
using Zygieldesk.Application.Functions.Responses;
using Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment;
using Zygieldesk.Application.Services;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment
{
    public class UpdateTicketCommentCommandHandler : IRequestHandler<UpdateTicketCommentCommand, UpdateTicketCommentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public UpdateTicketCommentCommandHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public async Task<UpdateTicketCommentCommandResponse> Handle(UpdateTicketCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateTicketCommentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request);

            if (!validatorResult.IsValid)
            {
                return new UpdateTicketCommentCommandResponse(validatorResult);
            }

            var ticketCommentToUpdate = await _ticketCommentRepository.GetByIdAsync(request.TicketCommentId);
            if (ticketCommentToUpdate == null)
            {
                return new UpdateTicketCommentCommandResponse(ResponseStatus.NotFound, $"Ticket comment with {request.TicketCommentId} id, does not exist",
                    validatorResult);
            }
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, ticketCommentToUpdate,
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                return new UpdateTicketCommentCommandResponse(ResponseStatus.Forbidden, "Forbidden", validatorResult);
            }

            ticketCommentToUpdate.CommentBody = request.CommentBody;

            await _ticketCommentRepository.UpdateAsync(ticketCommentToUpdate);

            return new UpdateTicketCommentCommandResponse("Ticket comment successfully updated", true);
        }
    }
}
