using BookingService.Common;
using BookingService.DtoModels;
using BookingService.DtoModels.Booking;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class BookingService
    {
        private BookingDB _db;
        private static HttpClient _client = new HttpClient();

        public BookingService(BookingDB db)
        {
            _db = db;
        }

        public async Task<HttpStatusCode> BookingHotels(RouteDto route)
        {
            var bookingIds = new List<int>();

            foreach (HotelDto hotel in route.Hotels)
            {
                var hotelUrl = BookingServiceUrls.HOTEL_URL;

                var data = new BookingHotelDto
                {
                    hotelId = hotel.HotelId,
                    personId = route.PersonId,
                    dateDeparture = hotel.DateDeparture,
                    dateArrive = hotel.DateArrive,
                    countOfPersons = route.CountOfPersonsAdults + route.CountOfPersonsChildren
                };
                var body = JsonConvert.SerializeObject(data);

                try
                {
                    var bookingId = await SendRequest(body, hotelUrl);
                    bookingIds.Add(bookingId);
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
                var transportUrl = BookingServiceUrls.TRANSPORT_URL + transport.TransportId.ToString();

                var data = new BookingTransportDto
                {
                    personId = route.PersonId,
                    countOfPersons = route.CountOfPersonsAdults + route.CountOfPersonsChildren                    
                };
                var body = JsonConvert.SerializeObject(data);

                try
                {
                    var bookingId = await SendRequest(body, transportUrl);
                    bookingIds.Add(bookingId);
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
                var eventUrl = BookingServiceUrls.EVENT_URL + ev.EventId.ToString();

                var data = new BookingEventDto
                {
                    userId = route.PersonId,
                    countOfPersonsAdults = route.CountOfPersonsAdults,
                    countOfPersonsChildren = route.CountOfPersonsChildren
                };
                var body = JsonConvert.SerializeObject(data);

                try
                {
                    var bookingId = await SendRequest(body, eventUrl);
                    bookingIds.Add(bookingId);
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

        public async Task<int> SendRequest(string body, string url)
        {
            var bookingId = 0;
            var response = await _client.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync();
                var booked = JsonConvert.DeserializeObject<BookingResponseDto>(await content);
                bookingId = booked.bookingId;
            }

            return bookingId;
        }
    }
}
