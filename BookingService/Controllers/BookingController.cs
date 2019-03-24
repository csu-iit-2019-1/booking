using BookingService.DtoModels;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        [HttpPost]
        [Route("route")]
        public void Route([FromBody] RouteDto route)
        {

        }
    }
}
