﻿using BookingService.Common;
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
            var logger = new Logger();
            await logger.WriteLogAsync($"{DateTime.Now} - Hotel purchase service triggered");

            var hotelKey = (userId, BookingType.Hotel);
            var hotelKeyIds = _db.Find(hotelKey);
            var isBuying = true;

            if (hotelKeyIds != null)
            {
                if (hotelKeyIds.Count != 0)
                {
                    foreach (var id in hotelKeyIds)
                    {
                        var hotelServiceUrl = BuyingServiceUrls.HOTEL_URL;
                        var hotelData = new HotelDto()
                        {
                            BookingId = id
                        };

                        var body = JsonConvert.SerializeObject(hotelData);
                        var response = await _client.PutAsync(hotelServiceUrl, new StringContent(body, Encoding.UTF8, "application/json"));

                        var responseData = false;
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            dynamic a = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
                            responseData = a.status;
                        }

                        if (responseData == true)
                        {
                            isBuying = true;
                            _db.Remove(hotelKey);
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
