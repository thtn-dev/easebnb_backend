using Easebnb.Application.Homestay.Commands;
using Easebnb.Application.Homestay.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Easebnb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomestaysController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateHomestayCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] GetHomestayByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
