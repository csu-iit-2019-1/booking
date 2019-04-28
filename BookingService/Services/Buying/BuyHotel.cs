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
    public class BuyHotel
    {
        private readonly static HttpClient _client = new HttpClient();
        private BookingDB _db;

        public BuyHotel(BookingDB db)
        {
            _db = db;
        }

        public async Task<bool> BuyHotelAsync(int userId)
        {
            var hotelKey = (userId, BookingType.Transport);
            var hotelKeyIds = _db.Find(hotelKey);
            var isBuying = true;

            foreach (var id in hotelKeyIds)
            {
                var hotelServiceUrl = BuyingServiceUrls.HOTEL_URL;
                var hotelData = new HotelDto()
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
                    _db.Remove(hotelKey);
                }
            }

            return isBuying;
        }
    }
}
