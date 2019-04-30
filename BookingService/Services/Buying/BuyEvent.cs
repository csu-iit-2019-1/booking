using BookingService.Common;
using BookingService.Controllers.Buying.Dtos;
using BookingService.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Services.Buying
{
    public class BuyEvent
    {
        private readonly static HttpClient _client = new HttpClient();
        private BookingDB _db;

        public BuyEvent(BookingDB db)
        {
            _db = db;
        }

        public async Task<bool> BuyEVentAsync(int userId)
        {
            var eventKey = (userId, BookingType.Event);
            var eventlKeyIds = _db.Find(eventKey);
            var isBuying = true;

            if (eventlKeyIds != null)
            {
                foreach (var id in eventlKeyIds)
                {
                    var hotelServiceUrl = BuyingServiceUrls.EVENT_URL;
                    var hotelData = new EventDto()
                    {
                        BookingId = id
                    };

                    var body = JsonConvert.SerializeObject(hotelData);
                    var response = await _client.PostAsync(hotelServiceUrl, new StringContent(body, Encoding.UTF8, "application/json"));

                    var responseData = await response.Content.ReadAsAsync<bool>();

                    if (responseData == true)
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
