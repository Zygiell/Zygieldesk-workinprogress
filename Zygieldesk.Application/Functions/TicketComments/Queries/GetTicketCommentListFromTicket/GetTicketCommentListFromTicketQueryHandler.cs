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
    public class GetTicketCommentListFromTicketQueryHandler : IRequestHandler<GetTicketCommentListFromTicketQuery, List<TicketCommentListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketCommentRepository _ticketCommentRepository;

        public GetTicketCommentListFromTicketQueryHandler(IMapper mapper, ITicketCommentRepository ticketCommentRepository)
        {
            _mapper = mapper;
            _ticketCommentRepository = ticketCommentRepository;
        }
        public async Task<List<TicketCommentListViewModel>> Handle(GetTicketCommentListFromTicketQuery request, CancellationToken cancellationToken)
        {
            var ticketComments = await _ticketCommentRepository.GetAllTicketCommentsFromTicketAsync(request.TicketId);

            if (ticketComments== null)
            {
                return null;
            }

            return _mapper.Map<List<TicketCommentListViewModel>>(ticketComments);
        }
    }
}
