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
    public class BuyTransport
    {
        private readonly static HttpClient _client = new HttpClient();
        private BookingDB _db;

        public BuyTransport(BookingDB db)
        {
            _db = db;
        }

        public async Task<bool> BuyTransportAsync(int userId)
        {
            var transportKey = (userId, BookingType.Transport);
            var bookingTransportIds = _db.Find(transportKey);
            var isBuying = true;

            foreach (var id in bookingTransportIds)
            {
                var transportServiceUrl = BuyingServiceUrls.TRANSPORT_URL;
                var transportData = new TransportDto()
                {
                    Id = id
                };

                var body = JsonConvert.SerializeObject(transportData); 
                var response = await _client.PostAsync(transportServiceUrl, new StringContent(body, Encoding.UTF8, "application/json"));

                if (response.StatusCode != HttpStatusCode.OK)
                {
                   return false;
                }
            }

            return isBuying;
        }
    }
}
