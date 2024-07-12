using Easebnb.Domain.Homestay;
using Easebnb.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Easebnb.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomestaysController : ApiController
    {
        private readonly IDomainEventDispatcher _dispatcher;
        public HomestaysController(IDomainEventDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            await Task.Delay(10);
            var hs = HomestayEntity.Create("Homestay 1", "Description 1");
            await _dispatcher.DispatchAndClearEvents([hs]);
            return Ok();
        }
    }
}
