using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Domain.Entities;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketViewModel>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;


        public GetTicketByIdQueryHandler(IMapper mapper, ITicketRepository ticketRepository)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }
        public async Task<TicketViewModel> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetByIdAsync(request.TicketId);
            

            return _mapper.Map<TicketViewModel>(ticket);

        }
    }
}
