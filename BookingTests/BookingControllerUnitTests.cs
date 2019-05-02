using BookingService.DtoModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace BookingTests
{
    public class BookingControllerUnitTests
    {
        HttpClient httpClient = new HttpClient();
        string bookingUrl = "https://csubookingservice.azurewebsites.net/booking/route";        

        [Fact]
        public async void RouteSuccess()
        {  
            var route = new RouteDto
            {
                Id = 100,
                PersonId = 200,
                Hotels = new List<HotelDto>
                {
                    new HotelDto
                    {
                        HotelId = 1                        
                    },
                    new HotelDto
                    {
                        HotelId = 2                        
                    }
                },
                Transports = new List<TransportDto>()
                {
                    new TransportDto
                    {
                        TransportId = 1,                        
                    }
                },
                Events = new List<EventDto>()
                {
                    new EventDto
                    {
                        EventId = 1                        
                    },
                    new EventDto
                    {
                        EventId = 2                        
                    }
                },
                CountOfPersonsAdults = 2,
                CountOfPersonsChildren = 1
            };

            var body = JsonConvert.SerializeObject(route);            
            var response = await httpClient.PostAsync(bookingUrl, new StringContent(body, Encoding.UTF8, "application/json"));
            var content = response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }        
    }
}
