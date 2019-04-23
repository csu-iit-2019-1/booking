using BookingService.Controllers.Buying.Dtos;
using BookingService.Models;
using BookingService.Services.Buying;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace BookingService.Controllers.Buying
{
    [Route("[controller]")]
    [ApiController]
    public class BuyingController : Controller
    {
        private BookingDB _db;
        private static HttpClient _client = new HttpClient();

        public BuyingController(BookingDB db)
        {
            _db = db;
        }

        [HttpPost]
        public async System.Threading.Tasks.Task<bool> BuyAsync([FromBody] UserDto dto)
        {
            var buyTransport = new BuyTransport(_db);

            return await buyTransport.BuyTransportAsync(dto.UserId);
        }
    }
}
