using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList
{
    public class GetTicketCommentListFromTicketQuery : IRequest<List<TicketCommentListViewModel>>
    {
        public int TicketId { get; set; }
    }
}
