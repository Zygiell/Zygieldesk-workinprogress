using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Contracts.Persistance;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;

namespace Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList
{
    public class GetTicketListFromCategoryQueryHandler : IRequestHandler<GetTicketListFromCategoryQuery, List<TicketListViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepository _ticketRepository;

        public GetTicketListFromCategoryQueryHandler(IMapper mapper, ITicketRepository ticketRepository)
        {
            _mapper = mapper;
            _ticketRepository = ticketRepository;
        }

        public async Task<List<TicketListViewModel>> Handle(GetTicketListFromCategoryQuery request, CancellationToken cancellationToken)
        {
            var tickets = await _ticketRepository.GetAllTicketsFromCategoryAsync(request.CategoryId);
            if(tickets == null)
            {
                return null;
            }

            return _mapper.Map<List<TicketListViewModel>>(tickets);
        }
    }
}
