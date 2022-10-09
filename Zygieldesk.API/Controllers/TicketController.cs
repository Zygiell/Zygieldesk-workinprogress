using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Functions.Responses;
using Zygieldesk.Application.Functions.Tickets.Commands.CreateTicket;
using Zygieldesk.Application.Functions.Tickets.Commands.DeleteTicket;
using Zygieldesk.Application.Functions.Tickets.Commands.UpdateTicket;
using Zygieldesk.Application.Functions.Tickets.Queries.GetAllTickets;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketById;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Update ticket
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Updates existing ticket.")]
        public async Task<ActionResult> UpdateTicket([FromBody] UpdateTicketCommand dto)
        {
            var response = await _mediator.Send(dto);

            return NoContent();
        }

        /// <summary>
        /// Delete ticket from database.
        /// </summary>
        /// <param name="id">Ticket to delete id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes existing ticket.")]
        public async Task<ActionResult> DeleteTicket(int id)
        {
            var response = await _mediator.Send(new DeleteTicketCommand() { TicketId = id });

            return NoContent();
        }

        /// <summary>
        /// Creates new ticket, existing category id from body dto has to be provided
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Creates new ticket.")]
        public async Task<ActionResult<CreateTicketCommandResponse>> CreateTicket([FromBody] CreateTicketCommand dto)
        {
            var response = await _mediator.Send(dto);

            return Ok(response.TicketId);
        }

        /// <summary>
        /// Get all tickets under provided category from database.
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
        [SwaggerOperation(Summary = "Returns all tickets associated with category id {categoryId}")]
        public async Task<ActionResult<List<CategoryListViewModel>>> GetAllTicketsFromCategory(int categoryId)
        {
            var response = await _mediator.Send(new GetTicketListFromCategoryQuery() { CategoryId = categoryId });

            return Ok(response);
        }

        /// <summary>
        /// Gets ticket by id from database
        /// </summary>
        /// <param name="id">Ticket id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns ticket from database with {id}")]
        public async Task<ActionResult<TicketViewModel>> GetTicketById(int id)
        {
            var response = await _mediator.Send(new GetTicketByIdQuery() { TicketId = id });

            return Ok(response);
        }

        /// <summary>
        /// Gets all tickets from database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Returns all tickets from database")]
        public async Task<ActionResult<List<TicketViewModel>>> GetAllTickets()
        {
            var response = await _mediator.Send(new GetAllTicketsQuery());

            return Ok(response);
        }
    }
}