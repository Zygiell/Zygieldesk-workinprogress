using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment;
using Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment;
using Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketCommentController : Controller
    {
        private readonly IMediator _mediator;

        public TicketCommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates new ticket comment.")]
        public async Task<ActionResult<CreateTicketCommentCommandResponse>> CreateTicketComment([FromBody] CreateTicketCommentCommand dto)
        {
            var response = await _mediator.Send(dto);

            return Ok(response.TicketCommentId);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Updates existing ticket comment")]
        public async Task<ActionResult> UpdateTicketComment([FromBody] UpdateTicketCommentCommand dto)
        {
            var response = await _mediator.Send(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete existing ticket.")]
        public async Task<ActionResult<DeleteTicketCommentCommandResponse>> DeleteTicketComment(int id)
        {
            var response = await _mediator.Send(new DeleteTicketCommentCommand() { TicketCommentId = id });

            return NoContent();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Returns all ticket comments from database.")]
        public async Task<ActionResult<List<TicketCommentViewModel>>> GetAllTicketComments()
        {
            var response = await _mediator.Send(new GetAllTicketsCommentsQuery());

            return Ok(response);
        }

        [HttpGet("ticket/{ticketId}")]
        [SwaggerOperation(Summary = "Returns all ticket comments associated with {ticketId}.")]
        public async Task<ActionResult<List<TicketCommentListViewModel>>> GetTicketCommentsFromTicket(int ticketId)
        {
            var response = await _mediator.Send(new GetTicketCommentListFromTicketQuery() { TicketId = ticketId });

            return Ok(response);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns ticket comment found by {id}.")]
        public async Task<ActionResult<TicketCommentViewModel>> GetTicketCommentById(int id)
        {
            var response = await _mediator.Send(new GetTicketCommentByIdQuery() { TicketCommentId = id });

            return Ok(response);
        }
    }
}