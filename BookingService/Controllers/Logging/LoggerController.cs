using BookingService.Services.LoggingrService;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookingService.Controllers.Logging
{
    [Route("[controller]")]
    [ApiController]
    public class LoggerController
    {
        [HttpGet]
        public async Task<string> ReadLogs()
        {
            var logger = new Logger();
            return await logger.ReadLogAsync();
        }
    }
}
