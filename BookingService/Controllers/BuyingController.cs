using Microsoft.AspNetCore.Mvc;

namespace BookingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BuyingController : Controller
    {
        [HttpPost("{userId}")]
        public void Post(int userId)
        {

        }
    }
}
