using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById
{
    public class GetTicketCommentByIdQuery : IRequest<TicketCommentViewModel>
    {
        public int TicketCommentId { get; set; }
    }
}
