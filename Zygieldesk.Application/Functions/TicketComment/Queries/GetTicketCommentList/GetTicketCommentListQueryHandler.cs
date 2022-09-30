using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList
{
    public class GetTicketCommentListQueryHandler : IRequestHandler<GetTicketCommentListQuery, List<TicketCommentListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;

        public GetTicketCommentListQueryHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
        }
        public async Task<List<TicketCommentListViewModel>> Handle(GetTicketCommentListQuery request, CancellationToken cancellationToken)
        {
            var ticketComments = await _ticketCommentRepository.GetAllAsync();

            return _mapper.Map<List<TicketCommentListViewModel>>(ticketComments);
        }
    }
}
