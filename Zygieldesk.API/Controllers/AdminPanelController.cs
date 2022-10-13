using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Account.Queries.GetUserByEmail;
using Zygieldesk.Application.Functions.AdminPanel.Commands.ChangeUserRole;
using Zygieldesk.Application.Functions.AdminPanel.Queries.GetAllUseres;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : Controller
    {
        private readonly IMediator _mediator;

        public AdminPanelController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("users")]
        [SwaggerOperation(Summary = "Returns all users from database")]
        public async Task<ActionResult<List<UserViewModel>>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());

            return Ok(users);
        }
        [HttpPut]
        [SwaggerOperation(Summary = "Change user role")]
        public async Task<ActionResult> ChangeUserRole([FromBody] ChangeUserRoleCommand dto)
        {
            var response = await _mediator.Send(dto);

            return Ok(response);
        }

    }
}
