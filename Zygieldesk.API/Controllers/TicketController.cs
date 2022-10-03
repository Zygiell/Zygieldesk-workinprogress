using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Functions.Ticket.Queries.GetAllTickets;
using Zygieldesk.Application.Functions.Ticket.Queries.GetTicketById;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Get all tickets under provided category from database.
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <returns></returns>
        [HttpGet("category/{categoryId}")]
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
