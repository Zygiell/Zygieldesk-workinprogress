using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Functions.Tickets.Queries.GetTicketList;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]/{categoryId}")]
    [ApiController]
    public class TicketController : Controller
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryListViewModel>>> GetAllTicketsFromCategory(int categoryId)
        {
            var ticketsListViewModel = await _mediator.Send(new GetTicketListQuery() { CategoryId = categoryId});
            if(ticketsListViewModel == null)
            {
                return NotFound($"Category with {categoryId} does not exist.");
            }

            return Ok(ticketsListViewModel);
        }
    }
}
