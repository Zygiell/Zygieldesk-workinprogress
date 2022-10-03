using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment
{
    public class CreateTicketCommentCommandHandler : IRequestHandler<CreateTicketCommentCommand, CreateTicketCommentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;
        private readonly ITicketRepository _ticketRepository;

        public CreateTicketCommentCommandHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository, ITicketRepository ticketRepository)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
            _ticketRepository = ticketRepository;
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
                return new CreateTicketCommentCommandResponse($"Ticket id {request.TicketId} you are trying to comment does not exist", false);
            }

            var ticketComment = _mapper.Map<TicketComment>(request);
            ticketComment = await _ticketCommentRepository.AddAsync(ticketComment);

            return new CreateTicketCommentCommandResponse(ticketComment.Id);
        }
    }
}
