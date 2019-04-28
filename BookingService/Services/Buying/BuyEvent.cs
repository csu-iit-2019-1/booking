using BookingService.Common;
using BookingService.Controllers.Buying.Dtos;
using BookingService.Models;
using Newtonsoft.Json;
using System.Net;
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
            var eventKey = (userId, BookingType.Transport);
            var eventlKeyIds = _db.Find(eventKey);
            var isBuying = true;

            foreach (var id in eventlKeyIds)
            {
                var hotelServiceUrl = BuyingServiceUrls.EVENT_URL;
                var hotelData = new EventDto()
                {
                    Id = id
                };

                var body = JsonConvert.SerializeObject(hotelData);
                var response = await _client.PostAsync(hotelServiceUrl, new StringContent(body, Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return false;
                }
                else
                {
                    _db.Remove(eventKey);
                }
            }

            return isBuying;
        }
    }
}
