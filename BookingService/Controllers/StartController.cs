using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StartController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return "Welcome to Booking Service";
        }
    }
}
