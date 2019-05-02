using BookingService.Common;
using BookingService.Controllers.Buying.Dtos;
using BookingService.Models;
using BookingService.Services.LoggingrService;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Services.BuyingService
{
    public class BuyEvent
    {
        private readonly static HttpClient _client = new HttpClient();
        private BookingDB _db;

        public BuyEvent(
            BookingDB db)
        {
            _db = db;
        }

        public async Task<bool> BuyEventAsync(int userId)
        {
            var logger = new Logger();
            await logger.WriteLogAsync($"{DateTime.Now} - Event purchase service triggered");

            var eventKey = (userId, BookingType.Event);
            var eventlKeyIds = _db.Find(eventKey);
            var isBuying = true;

            if (eventlKeyIds.Count != 0)
            {
                foreach (var id in eventlKeyIds)
                {
                    var eventServiceUrl = BuyingServiceUrls.EVENT_URL;
                    var hotelData = new EventDto()
                    {
                        BookingId = id
                    };

                    var body = JsonConvert.SerializeObject(hotelData);
                    var response = await _client.PostAsync(eventServiceUrl, new StringContent(body, Encoding.UTF8, "application/json"));

                    var responseData = "";
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }

                    if (responseData == "true")
                    {
                        isBuying = true;
                        _db.Remove(eventKey);
                    }
                    else
                    {
                        isBuying = false;
                    }
                }
            }
            else
            {
                isBuying = false;
            }

            return isBuying;
        }
    }
}
