﻿using BookingService.Common;
using BookingService.DtoModels;
using BookingService.DtoModels.Booking;
using BookingService.Services.LoggingrService;
using Newtonsoft.Json;
using System;
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
        private Logger logger = new Logger();

        public BookingService(BookingDB db)
        {
            _db = db;
        }

        public async Task<HttpStatusCode> BookingHotels(RouteDto route)
        {            
            await logger.WriteLogAsync($"{DateTime.Now} - A request has come for booking a hotels");

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
                    await logger.WriteLogAsync($"{DateTime.Now} - Hotel service is not available");
                    return HttpStatusCode.ServiceUnavailable;
                }
            }

            var key = (route.PersonId, BookingType.Hotel);
            _db.AddOrUpdate(key, bookingIds);

            await logger.WriteLogAsync($"{DateTime.Now} - The hotel booking data is preserved in the database");
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> BookingTransports(RouteDto route)
        {
            await logger.WriteLogAsync($"{DateTime.Now} - A request has come for booking a transports");

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
                    await logger.WriteLogAsync($"{DateTime.Now} - Transport service is not available");
                    return HttpStatusCode.ServiceUnavailable;
                }
            }

            var key = (route.PersonId, BookingType.Transport);
            _db.AddOrUpdate(key, bookingIds);

            await logger.WriteLogAsync($"{DateTime.Now} - The transport booking data is preserved in the database");
            return HttpStatusCode.OK;
        }

        public async Task<HttpStatusCode> BookingEvents(RouteDto route)
        {
            await logger.WriteLogAsync($"{DateTime.Now} - A request has come for booking a events");

            var bookingIds = new List<int>();

            foreach (EventDto ev in route.Events)
            {
                var eventUrl = BookingServiceUrls.EVENT_URL;

                var data = new BookingEventDto
                {
                    userId = route.PersonId,
                    eventId = ev.EventId,
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
                    await logger.WriteLogAsync($"{DateTime.Now} - Event service is not available");
                    return HttpStatusCode.ServiceUnavailable;
                }
            }

            var key = (route.PersonId, BookingType.Event);
            _db.AddOrUpdate(key, bookingIds);

            await logger.WriteLogAsync($"{DateTime.Now} - The event booking data is preserved in the database");
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
