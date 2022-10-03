using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketCommentController : Controller
    {
        private readonly IMediator _mediator;

        public TicketCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ticket/{ticketId}")]
        [SwaggerOperation(Summary = "Returns all ticket comments associated with {ticketId}.")]
        public async Task<ActionResult<List<TicketCommentListViewModel>>> GetTicketCommentsFromTicket(int ticketId)
        {
            var ticketCommentsListViewModel = await _mediator.Send(new GetTicketCommentListFromTicketQuery() { TicketId = ticketId});
            if(ticketCommentsListViewModel == null)
            {
                return NotFound($"Ticket with {ticketId} id does not exist.");
            }

            return Ok(ticketCommentsListViewModel);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns ticket comment found by {id}.")]

        public async Task<ActionResult<TicketCommentViewModel>> GetTicketCommentById(int id)
        {
            var ticketComment = await _mediator.Send(new GetTicketCommentByIdQuery() { TicketCommentId = id });
            if (ticketComment == null)
            {
                return NotFound($"Ticket comment with {id} id does not exist.");
            }
            return Ok(ticketComment);
        }
    }
}
