using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Functions.Categories.Commands.DeleteCategory;
using Zygieldesk.Application.Functions.Categories.Commands.UpdateCategory;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Creates new category.")]
        public async Task<ActionResult<CreatedCategoryCommandResponse>> CreateCategory([FromBody] CreatedCategoryCommand dto)
        {
            var response = await _mediator.Send(dto);

            return Ok(response.CategoryId);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Updates existing category")]
        public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryCommand dto)
        {
            var response = await _mediator.Send(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes existing category")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var response = await _mediator.Send(new DeleteCategoryCommand() { CategoryId = id });

            return NoContent();
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Returns all categories from database")]
        public async Task<ActionResult<List<CategoryListViewModel>>> GetAllCategories()
        {
            var categoryListViewModel = await _mediator.Send(new GetCategoryListQuery());

            return Ok(categoryListViewModel);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns category found by {id} with tickets associated with category.")]
        public async Task<ActionResult<CategoryWithTitcketsViewModel>> GetCategoryWithTickets(int id)
        {
            var dto = await _mediator.Send(new GetCategoryWithTicketsQuery { CategoryId = id });

            return Ok(dto);
        }
    }
}