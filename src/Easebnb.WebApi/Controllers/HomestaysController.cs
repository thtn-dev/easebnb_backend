using Easebnb.Application.Homestay.Commands;
using Easebnb.Domain.Homestay;
using Easebnb.Shared;
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
    }
}
