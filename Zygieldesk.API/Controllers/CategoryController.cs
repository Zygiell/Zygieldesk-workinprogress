using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Categories.Commands.CreateCategory;
using Zygieldesk.Application.Functions.Categories.Commands.DeleteCategory;
using Zygieldesk.Application.Functions.Categories.Commands.UpdateCategory;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryList;
using Zygieldesk.Application.Functions.Categories.Queries.GetCategoryWithTickets;
using Zygieldesk.Application.Functions.Responses;

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

        /// <summary>
        /// Get all the categories from database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerOperation(Summary ="Returns all categories from database")]
        public async Task<ActionResult<List<CategoryListViewModel>>> GetAllCategories()
        {
            var categoryListViewModel = await _mediator.Send(new GetCategoryListQuery());
            return Ok(categoryListViewModel);
        }

        /// <summary>
        /// Get category by id with tickets list.
        /// </summary>
        /// <param name="id">Id of the category</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Returns category found by {id} with tickets associated with category.")]
        public async Task<ActionResult<CategoryWithTitcketsViewModel>> GetCategoryWithTickets(int id)
        {
            var dto = await _mediator.Send(new GetCategoryWithTicketsQuery { CategoryId = id });

            return Ok(dto);
        }

        /// <summary>
        /// Create new category.
        /// </summary>
        /// <param name="dto">Model received from body</param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerOperation(Summary = "Creates new category.")]
        public async Task<ActionResult<CreatedCategoryCommandResponse>> CreateCategory([FromBody] CreatedCategoryCommand dto)
        {
            var response = await _mediator.Send(dto);

            if (response.ValidationErrors.Any())
            {
                return BadRequest(response.ValidationErrors);
            }
            if (response.Status == ResponseStatus.Forbidden)
            {
                return StatusCode(403, response.Message);
            }

            return Ok(response.CategoryId);
        }

        /// <summary>
        /// Updates existing category, valid category id provided from body is required.
        /// </summary>
        /// <param name="updateCategoryCommand"></param>
        /// <returns></returns>
        [HttpPut]
        [SwaggerOperation(Summary = "Updates existing category")]
        public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            var categoryWasFound = await _mediator.Send(updateCategoryCommand);
            if (categoryWasFound.Status == ResponseStatus.NotFound)
            {
                return NotFound(categoryWasFound.Message);
            }
            if (categoryWasFound.ValidationErrors.Any())
            {
                return BadRequest(categoryWasFound.ValidationErrors);
            }

            if (categoryWasFound.Status == ResponseStatus.Forbidden)
            {
                return StatusCode(403, categoryWasFound.Message);
            }


            return NoContent();
        }
        /// <summary>
        /// Delete category by id.
        /// </summary>
        /// <param name="id">Id of category to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Deletes existing category")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var deleteCategoryCommand = new DeleteCategoryCommand() { CategoryId=id };
            var categoryWasFound = await _mediator.Send(deleteCategoryCommand);
            if (categoryWasFound.Status == ResponseStatus.NotFound)
            {
                return NotFound(categoryWasFound.Message);
            }
            if (categoryWasFound.Status == ResponseStatus.Forbidden)
            {
                return StatusCode(403, categoryWasFound.Message);
            }

            return NoContent();
        }

    }
}
