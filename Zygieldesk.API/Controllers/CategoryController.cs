using MediatR;
using Microsoft.AspNetCore.Mvc;
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

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<CategoryWithTitcketsViewModel>> GetCategoryWithTickets(int id)
        {
            var dto = await _mediator.Send(new GetCategoryWithTicketsQuery { CategoryId = id });

            if(dto == null)
            {
                return NotFound($"Category with {id} id does not exist");
            }

            return Ok(dto);
        }

        /// <summary>
        /// Create new category.
        /// </summary>
        /// <param name="dto">Model received from body</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CreatedCategoryCommandResponse>> CreateCategory([FromBody] CreatedCategoryCommand dto)
        {
            var response = await _mediator.Send(dto);
            return Ok(response.CategoryId);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            var categoryWasFound = await _mediator.Send(updateCategoryCommand);
            if (!categoryWasFound.Success)
            {
                return NotFound(categoryWasFound.Message);
            }


            return NoContent();
        }
        /// <summary>
        /// Delete category by id.
        /// </summary>
        /// <param name="id">Id of category to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var deleteCategoryCommand = new DeleteCategoryCommand() { CategoryId=id };
            var categoryWasFound = await _mediator.Send(deleteCategoryCommand);
            if (!categoryWasFound.Success)
            {
                return NotFound(categoryWasFound.Message);
            }
            return NoContent();
        }

    }
}
