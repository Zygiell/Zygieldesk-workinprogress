using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Responses;
using Zygieldesk.Application.Functions.TicketComments.Commands.CreateTicketComment;
using Zygieldesk.Application.Functions.TicketComments.Commands.DeleteTicketComment;
using Zygieldesk.Application.Functions.TicketComments.Commands.UpdateTicketComment;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetAllTicketsComments;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommentsList;
using Zygieldesk.Application.Functions.TicketComments.Queries.GetTicketCommetById;
using Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket;

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


        [HttpPut]
        [SwaggerOperation(Summary = "Updates existing ticket comment")]
        public async Task<ActionResult> UpdateTicketComment([FromBody]UpdateTicketCommentCommand updateTicketCommentCommand)
        {
            var ticketCommentWasFound = await _mediator.Send(updateTicketCommentCommand);
            if (ticketCommentWasFound.ValidationErrors.Any())
            {
                return BadRequest(ticketCommentWasFound);
            }
            if (ticketCommentWasFound.Status == ResponseStatus.NotFound)
            {
                return NotFound(ticketCommentWasFound.Message);
            }
            if (ticketCommentWasFound.Status == ResponseStatus.Forbidden)
            {
                return StatusCode(403, ticketCommentWasFound.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete existing ticket.")]
        public async Task<ActionResult<DeleteTicketCommentCommandResponse>> DeleteTicketComment(int id)
        {
            var ticketCommentWasFound = await _mediator.Send(new DeleteTicketCommentCommand() { TicketCommentId = id });

            if (ticketCommentWasFound.Status == ResponseStatus.NotFound)
            {
                return NotFound(ticketCommentWasFound.Message);
            }
            if (ticketCommentWasFound.Status == ResponseStatus.Forbidden)
            {
                return StatusCode(403, ticketCommentWasFound.Message);
            }

            return NoContent();
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates new ticket comment.")]
        public async Task<ActionResult<CreateTicketCommentCommandResponse>> CreateTicketComment([FromBody]CreateTicketCommentCommand dto)
        {
            var response = await _mediator.Send(dto);
            if (response.ValidationErrors.Any())
            {
                return BadRequest(response.ValidationErrors);
            }
            if (response.Status == ResponseStatus.NotFound)
            {
                return NotFound(response.Message);
            }
            if (response.Status == ResponseStatus.Forbidden)
            {
                return StatusCode(403, response.Message);
            }

            return Ok(response.TicketCommentId);
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Returns all ticket comments from database.")]
        public async Task<ActionResult<List<TicketCommentViewModel>>> GetAllTicketComments()
        {
            var ticketCommentsList = await _mediator.Send(new GetAllTicketsCommentsQuery());

            return Ok(ticketCommentsList);
        }


        [HttpGet("ticket/{ticketId}")]
        [SwaggerOperation(Summary = "Returns all ticket comments associated with {ticketId}.")]
        public async Task<ActionResult<List<TicketCommentListViewModel>>> GetTicketCommentsFromTicket(int ticketId)
        {
            var ticketCommentsListViewModel = await _mediator.Send(new GetTicketCommentListFromTicketQuery() { TicketId = ticketId});

            return Ok(ticketCommentsListViewModel);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns ticket comment found by {id}.")]

        public async Task<ActionResult<TicketCommentViewModel>> GetTicketCommentById(int id)
        {
            var ticketComment = await _mediator.Send(new GetTicketCommentByIdQuery() { TicketCommentId = id });

            return Ok(ticketComment);
        }
    }
}
