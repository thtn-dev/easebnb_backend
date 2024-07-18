using Easebnb.Application.User.Commands;
using Easebnb.Application.User.Queries;
using Easebnb.Domain.Common.Services;
using Easebnb.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Easebnb.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly ISystemIdGenService _systemIdGenService;
        public AuthController(ISystemIdGenService systemIdGenService)
        {
            _systemIdGenService = systemIdGenService;
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

        [HttpGet]
        public async Task<IActionResult> GetIdGen()
        {
            await Task.Delay(10);
            var data = new
            {
                longId = _systemIdGenService.GenerateId<long>(),
                stringId = _systemIdGenService.GenerateId<string>()
            };
            return Ok(data);
        }
    }
}
