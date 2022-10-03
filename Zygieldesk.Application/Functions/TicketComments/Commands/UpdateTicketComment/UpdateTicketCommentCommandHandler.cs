using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment
{
    public class UpdateTicketCommentCommandHandler : IRequestHandler<UpdateTicketCommentCommand, UpdateTicketCommentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;

        public UpdateTicketCommentCommandHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
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
                return new UpdateTicketCommentCommandResponse($"Ticket comment with {request.TicketCommentId} id, does not exist", false);
            }

            ticketCommentToUpdate.CommentBody = request.CommentBody;

            await _ticketCommentRepository.UpdateAsync(ticketCommentToUpdate);

            return new UpdateTicketCommentCommandResponse("Ticket comment successfully updated", true);
        }
    }
}
