using BookingService.DtoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BookingTests
{
    public class BookingControllerUnitTests
    {
        [Fact]
        public async void RouteSuccess()
        {
            var httpClient = new HttpClient();
            var bookingUrl = "http://localhost:5000/booking/route";

            var route = new RouteDto
            {
                Id = 100,
                PersonId = 200,
                Hotels = new List<HotelDto>
                {
                    new HotelDto
                    {
                        HotelId = 100,
                        Name = "New Hotel"
                    },
                    new HotelDto
                    {
                        HotelId = 101,
                        Name = "Райское Наслаждение"
                    }
                },
                Transports = new List<TransportDto>()
                {
                    new TransportDto
                    {
                        TransportId = 200,                        
                    }
                },
                Events = new List<EventDto>()
                {
                    new EventDto
                    {
                        EventId = 300,
                        Name = "Rock concert"
                    },
                    new EventDto
                    {
                        EventId = 301,
                        Name = "Opera"
                    }
                },
                CountOfPersonsAdults = 2,
                CountOfPersonsChildren = 1
            };

            var body = JsonConvert.SerializeObject(route);            
            var response = await httpClient.PostAsync(bookingUrl, new StringContent(body, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
