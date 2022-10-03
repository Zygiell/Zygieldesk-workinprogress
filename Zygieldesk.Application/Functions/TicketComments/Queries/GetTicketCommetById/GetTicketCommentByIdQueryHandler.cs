using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById
{
    public class GetTicketCommentByIdQueryHandler : IRequestHandler<GetTicketCommentByIdQuery, TicketCommentViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;

        public GetTicketCommentByIdQueryHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
        }
        public async Task<TicketCommentViewModel> Handle(GetTicketCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var ticketComment = await _ticketCommentRepository.GetByIdAsync(request.TicketCommentId);

            return _mapper.Map<TicketCommentViewModel>(ticketComment);
        }
    }
}
