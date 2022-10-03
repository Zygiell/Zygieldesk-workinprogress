using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Ticket.Queries.GetTicketById;

namespace Zygieldesk.Application.Functions.Ticket.Queries.GetAllTickets
{
    public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, List<TicketViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;

        public GetAllTicketsQueryHandler(IMapper mapper, ITicketRepository ticketRepository)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }
        public async Task<List<TicketViewModel>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            var ticketList = await _ticketRepository.GetAllAsync();

            return _mapper.Map<List<TicketViewModel>>(ticketList);
        }
    }
}
