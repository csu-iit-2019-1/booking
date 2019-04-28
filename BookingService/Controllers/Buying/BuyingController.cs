using BookingService.Controllers.Buying.Dtos;
using BookingService.Models;
using BookingService.Services.Buying;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookingService.Controllers.Buying
{
    [Route("[controller]")]
    [ApiController]
    public class BuyingController : Controller
    {
        private readonly BookingDB _db;
        private static readonly HttpClient _client = new HttpClient();

        public BuyingController(BookingDB db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("transport")]
        public async Task<bool> BuyTransportAsync([FromBody] UserDto dto)
        {
            var buyTransport = new BuyTransport(_db);

            return await buyTransport.BuyTransportAsync(dto.UserId);
        }

        [HttpPost]
        [Route("hotel")]
        public async Task<bool> BuyHotelAsync([FromBody] UserDto dto)
        {
            var buyTransport = new BuyHotel(_db);

            return await buyTransport.BuyHotelAsync(dto.UserId);
        }

        [HttpPost]
        [Route("event")]
        public async Task<bool> BuyEventAsync([FromBody] UserDto dto)
        {
            var buyTransport = new BuyTransport(_db);

            return await buyTransport.BuyTransportAsync(dto.UserId);
        }
    }
}
