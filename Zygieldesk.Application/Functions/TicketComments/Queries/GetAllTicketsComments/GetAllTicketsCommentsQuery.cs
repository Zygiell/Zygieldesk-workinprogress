using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments
{
    public class GetAllTicketsCommentsQuery : IRequest<List<TicketCommentViewModel>>
    {

    }
}
