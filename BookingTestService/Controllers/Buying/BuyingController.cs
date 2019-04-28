using BookingTestService.Controllers.Buying.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BookingTestService.Controllers.Buying
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyingController : ControllerBase
    {
        [HttpPost]
        [Route("transport")]
        public bool BuyTransport([FromBody] TransportDto dto)
        {
            var isBuying = false;
            var buyingTransportId = dto.Id;

            if (buyingTransportId != -1)
            {
                isBuying = true;
            }

            return isBuying;
        }

        [HttpPost]
        [Route("hotel")]
        public bool BuyHotel([FromBody] HotelDto dto)
        {
            var isBuying = false;
            var buyingHotelId = dto.Id;

            if (buyingHotelId != -1)
            {
                isBuying = true;
            }

            return isBuying;
        }

        [HttpPost]
        [Route("event")]
        public bool BuyEvent([FromBody] EventDto dto)
        {
            var isBuying = false;
            var buyingEventlId = dto.Id;

            if (buyingEventlId != -1)
            {
                isBuying = true;
            }

            return isBuying;
        }
    }
}