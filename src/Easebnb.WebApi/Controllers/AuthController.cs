using Easebnb.Application.User.Commands;
using Easebnb.Application.User.Queries;
using Easebnb.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Easebnb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public class AuthController : ApiController
    {
        public AuthController()
        {
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            var result = await Mediator.Send(command);
            return result.Match(_ => Ok(), Problem);
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login(LoginQuery query)
        {
            var result = await Mediator.Send(query);
            return result.Match(Ok, Problem);
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
