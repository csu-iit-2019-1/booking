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
            var logger = new Logger();
            await logger.WriteLogAsync($"{DateTime.Now} - Transport purchase service triggered");

            var transportKey = (userId, BookingType.Transport);
            var bookingTransportIds = _db.Find(transportKey);
            var isBuying = true;

            if (bookingTransportIds != null)
            {
                if (bookingTransportIds.Count != 0)
                {
                    foreach (var id in bookingTransportIds)
                    {
                        var transportServiceUrl = BuyingServiceUrls.TRANSPORT_URL + id;
                        var transportData = new TransportDto()
                        {
                            BookingId = id
                        };

                        var body = JsonConvert.SerializeObject(transportData);
                        var response = await _client.GetAsync(transportServiceUrl);

                        var responseData = "";
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            responseData = await response.Content.ReadAsStringAsync();
                        }

                        if (responseData == "true" || responseData == "True")
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
            }
            else
            {
                isBuying = false;
            }

            return isBuying;
        }
    }
}
