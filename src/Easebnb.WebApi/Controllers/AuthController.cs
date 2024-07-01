using Easebnb.Application.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Easebnb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login()
        {
            await Task.Delay(10);
            return Ok();
        }

        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            await Task.Delay(10);
            return Ok();
        }

        [HttpPost(nameof(RefreshToken))]
        public async Task<IActionResult> RefreshToken()
        {
            await Task.Delay(10);
            return Ok();
        }

        [HttpPost(nameof(RevokeToken))]
        public async Task<IActionResult> RevokeToken()
        {
            await Task.Delay(10);
            return Ok();
        }

    }
}
