using BookingService.DtoModels;
using BookingService.DtoModels.Booking;
using BookingService.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private BookingDB _db;
        private static HttpClient _client = new HttpClient();
        private const string HOTEL_URL = "http://localhost:6001/api/booking/";
        private const string TRANSPORT_URL = "http://localhost:6001/api/booking/";
        private const string EVENT_URL = "http://localhost:6001/api/booking/";

        public BookingController(BookingDB db)
        {
            _db = db;
        }

        [HttpPost]
        [Route("route")]
        public async Task<ActionResult> Route([FromBody] RouteDto route)
        {
            var transportStatusCode = await BookingTransports(route);
            var hotelStatusCode = await BookingHotels(route);
            var eventStatusCode = await BookingEvents(route);

            if (transportStatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return Json(new { message = "Service Transport Unavailable" });
            }

            if (hotelStatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return Json(new { message = "Service Hotel Unavailable" });
            }

            if (eventStatusCode == HttpStatusCode.ServiceUnavailable)
            {
                return Json(new { message = "Service Event Unavailable" });
            }

            return StatusCode(200);
        }


        public async Task<HttpStatusCode> BookingHotels(RouteDto route)
        {
            var bookingIds = new List<int>();

            foreach (HotelDto hotel in route.Hotels)
            {
                var hotelUrl = HOTEL_URL + hotel.HotelId.ToString();

                var data = new BookingHotelDto
                {
                    HotelId = hotel.HotelId,
                    PersonId = route.PersonId,
                    DateDeparture = hotel.DateDeparture,
                    DateArrive = hotel.DateArrive,
                    CountOfPersons = route.CountOfPersonsAdults + route.CountOfPersonsChildren
                };
                var body = JsonConvert.SerializeObject(data);

                try
                {
                    bookingIds = await SendRequest(body, hotelUrl);
                }
                catch (HttpRequestException)
                {
                    return HttpStatusCode.ServiceUnavailable;
                }
            }

            var key = (route.PersonId, BookingType.Hotel);
            _db.AddOrUpdate(key, bookingIds);

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> BookingTransports(RouteDto route)
        {
            var bookingIds = new List<int>();

            foreach (TransportDto transport in route.Transports)
            {
                var transportUrl = TRANSPORT_URL + transport.TransportId.ToString();

                var data = new BookingTransportDto
                {
                    TransportId = transport.TransportId,
                    PersonId = route.PersonId,
                    CountOfAdults = route.CountOfPersonsAdults,
                    CountOfKids = route.CountOfPersonsChildren
                };
                var body = JsonConvert.SerializeObject(data);

                try
                {
                    bookingIds = await SendRequest(body, transportUrl);
                }
                catch (HttpRequestException)
                {
                    return HttpStatusCode.ServiceUnavailable;
                }
            }

            var key = (route.PersonId, BookingType.Transport);
            _db.AddOrUpdate(key, bookingIds);

            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> BookingEvents(RouteDto route)
        {
            var bookingIds = new List<int>();

            foreach (EventDto ev in route.Events)
            {
                var eventUrl = EVENT_URL + ev.EventId.ToString();

                var data = new BookingEventDto
                {
                    EventId = ev.EventId,
                    UserId = route.PersonId,
                    CountOfPersonsAdults = route.CountOfPersonsAdults,
                    CountOfPersonsChildren = route.CountOfPersonsChildren
                };
                var body = JsonConvert.SerializeObject(data);

                try
                {
                    bookingIds = await SendRequest(body, eventUrl);
                }
                catch (HttpRequestException)
                {
                    return HttpStatusCode.ServiceUnavailable;
                }
            }

            var key = (route.PersonId, BookingType.Event);
            _db.AddOrUpdate(key, bookingIds);

            return HttpStatusCode.OK;
        }

        private async Task<List<int>> SendRequest(string body, string url)
        {
            var bookingIds = new List<int>();

            var response = await _client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsAsync<int>();
                bookingIds.Add(content.Result);
            }

            return bookingIds;
        }
    }
}
