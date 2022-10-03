using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;

namespace Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment
{
    public class DeleteTicketCommentCommandHandler : IRequestHandler<DeleteTicketCommentCommand, DeleteTicketCommentCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;

        public DeleteTicketCommentCommandHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
        }
        public async Task<DeleteTicketCommentCommandResponse> Handle(DeleteTicketCommentCommand request, CancellationToken cancellationToken)
        {
            var ticketCommentToDelete = await _ticketCommentRepository.GetByIdAsync(request.TicketCommentId);

            if(ticketCommentToDelete == null)
            {
                return new DeleteTicketCommentCommandResponse($"Ticket with {request.TicketCommentId} id, does not exist", false);
            }

            await _ticketCommentRepository.DeleteAsync(ticketCommentToDelete);

            return new DeleteTicketCommentCommandResponse();
        }
    }
}
