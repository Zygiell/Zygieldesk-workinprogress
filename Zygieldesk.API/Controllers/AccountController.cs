using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Account.Commands.AddUser;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register new user account.")]
        public async Task<ActionResult<CreateUserCommandResponse>> CreateUserAccount([FromBody]CreateUserCommand dto)
        {
            var response = await _mediator.Send(dto);
            if(response.ValidationErrors.Any())
            {
                return BadRequest(response.ValidationErrors);
            }

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return Ok(response.Message);
        }


    }
}
