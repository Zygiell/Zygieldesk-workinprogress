using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList
{
    public class GetTicketCommentListQuery : IRequest<List<TicketCommentListViewModel>>
    {

    }
}
