using BookingService.DtoModels;
using BookingService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private BookingDB _db;
        private static HttpClient _client = new HttpClient();

        public BookingController(BookingDB db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("route")]
        public async Task<int> Route([FromBody] RouteDto route)
        {
            var booking = new Models.BookingService(_db);
            var randomizer = new Random();
            var transportStatusCode = await booking.BookingTransports(route);
            var hotelStatusCode = await booking.BookingHotels(route);
            var eventStatusCode = await booking.BookingEvents(route);

            if (transportStatusCode == HttpStatusCode.ServiceUnavailable ||
                hotelStatusCode == HttpStatusCode.ServiceUnavailable ||
                eventStatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return 0;
            }                        

            return randomizer.Next(1,100);
        }
    }
}
