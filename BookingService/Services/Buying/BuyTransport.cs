using BookingService.Common;
using BookingService.Controllers.Buying.Dtos;
using BookingService.Models;
using Newtonsoft.Json;
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

            if (bookingTransportIds != null)
            {
                foreach (var id in bookingTransportIds)
                {
                    var transportServiceUrl = BuyingServiceUrls.TRANSPORT_URL;
                    var transportData = new TransportDto()
                    {
                        BookingId = id
                    };

                    var body = JsonConvert.SerializeObject(transportData);
                    var response = await _client.PostAsync(transportServiceUrl, new StringContent(body, Encoding.UTF8, "application/json"));

                    var responseData = await response.Content.ReadAsAsync<bool>();

                    if (responseData == true)
                    {
                        isBuying = true;
                        _db.Remove(transportKey);
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
