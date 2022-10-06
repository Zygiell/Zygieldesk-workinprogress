using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
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
        /// <param name="updateTicketCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Updates existing ticket.")]
        public async Task<ActionResult> UpdateTicket([FromBody] UpdateTicketCommand updateTicketCommand)
        {
            var ticketWasFound = await _mediator.Send(updateTicketCommand);

            if (ticketWasFound.ValidationErrors.Any())
            {
                return BadRequest(ticketWasFound.ValidationErrors);
            }

            if (!ticketWasFound.Success)
            {
                return NotFound(ticketWasFound.Message);
            }

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
            var ticketWasFound = await _mediator.Send(new DeleteTicketCommand() { TicketId = id });

            if (!ticketWasFound.Success)
            {
                return NotFound(ticketWasFound.Message);
            }
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

            if (response.ValidationErrors.Any())
            {
                return BadRequest(response.ValidationErrors);
            }

            if (!response.Success)
            {
                return NotFound(response.Message);
            }

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
            var ticketsListViewModel = await _mediator.Send(new GetTicketListFromCategoryQuery() { CategoryId = categoryId});
            if(ticketsListViewModel == null)
            {
                return NotFound($"Category with {categoryId} id does not exist.");
            }

            return Ok(ticketsListViewModel);
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
            var ticket = await _mediator.Send(new GetTicketByIdQuery() { TicketId = id });
            if(ticket == null)
            {
                return NotFound($"Ticket with {id} id does not exist.");
            }

            return Ok(ticket);
        }

        /// <summary>
        /// Gets all tickets from database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Summary = "Returns all tickets from database")]
        public async Task<ActionResult<List<TicketViewModel>>> GetAllTickets()
        {
            var ticketsList = await _mediator.Send(new GetAllTicketsQuery());
            if(ticketsList == null)
            {
                return NotFound("There are not any tickets in database");
            }

            return Ok(ticketsList);
        }
    }
}
