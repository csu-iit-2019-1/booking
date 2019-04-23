using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingTestService.Controllers.Buying.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingTestService.Controllers.Buying
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyingController : ControllerBase
    {
        [HttpPost]
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
    }
}