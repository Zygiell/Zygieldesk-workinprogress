using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zygieldesk.Application.Functions.Account.Commands.AddUser;
using Zygieldesk.Application.Functions.Account.Commands.LoginUser;

namespace Zygieldesk.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [SwaggerOperation(Summary = "Login to user account.")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginUserCommandResponse>> Login([FromBody]LoginUserCommand loginUserCommand)
        {
            var response = await _mediator.Send(loginUserCommand);
            if (response.ValidationErrors.Any())
            {
                return BadRequest(response.ValidationErrors);
            }
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            // Returns jwtToken string
            return Ok(response.Message);
        }


        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register new user account.")]
        [AllowAnonymous]
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
