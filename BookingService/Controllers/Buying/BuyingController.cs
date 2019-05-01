using BookingService.Controllers.Buying.Dtos;
using BookingService.Models;
using BookingService.Services.BuyingService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
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
        [Route("buyout")]
        public async Task<bool> Buyout([FromBody] UserDto dto)
        {
            var isBuyed = true;

            var buyTransport = new BuyTransport(_db);
            var buyHotel = new BuyHotel(_db);
            var buyEvent = new BuyEvent(_db);

            var responseFromTransportService = await buyTransport.BuyTransportAsync(dto.UserId);
            var responseFromHotelService = await buyHotel.BuyHotelAsync(dto.UserId);
            var responseFromEventService = await buyEvent.BuyEventAsync(dto.UserId);

            if(!responseFromTransportService || !responseFromHotelService || !responseFromEventService)
            {
                isBuyed = false;
            }

            return isBuyed;
        }
    }
}
