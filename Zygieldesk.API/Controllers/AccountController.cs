using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Zygieldesk.Application.Functions.Account.Commands.AddUser;
using Zygieldesk.Application.Functions.Account.Commands.LoginUser;
using Zygieldesk.Application.Functions.Account.Commands.UpdateUser;

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
        public async Task<ActionResult<LoginUserCommandResponse>> Login([FromBody] LoginUserCommand loginUserCommand)
        {
            var response = await _mediator.Send(loginUserCommand);

            return Ok(response.Message);
        }

        [HttpPost("register")]
        [SwaggerOperation(Summary = "Register new user account.")]
        [AllowAnonymous]
        public async Task<ActionResult<CreateUserCommandResponse>> CreateUserAccount([FromBody] CreateUserCommand dto)
        {
            var response = await _mediator.Send(dto);

            return Ok(response.Message);
        }

        [HttpPut("update")]
        [SwaggerOperation(Summary = "Update existing user account.")]        
        public async Task<ActionResult> UpdateUserAccount([FromBody] UpdateUserCommand dto)
        {
            var response = await _mediator.Send(dto);

            return NoContent();
        }
    }
}