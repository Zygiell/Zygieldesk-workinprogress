using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketComments
{
    public class GetAllTicketsCommentsQueryHandler : IRequestHandler<GetAllTicketsCommentsQuery, List<TicketCommentViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;

        public GetAllTicketsCommentsQueryHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
        }
        public async Task<List<TicketCommentViewModel>> Handle(GetAllTicketsCommentsQuery request, CancellationToken cancellationToken)
        {
            var ticketCommentsList = await _ticketCommentRepository.GetAllAsync();

            return _mapper.Map<List<TicketCommentViewModel>>(ticketCommentsList);
        }
    }
}
