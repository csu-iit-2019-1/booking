using System.Collections.Generic;

namespace BookingService.DtoModels
{
    public class RouteDto
    {
        public int Id { get; }
        public List<TransportDto> Transports { get; }
        public List<HotelDto> Hotels { get; }
        public List<EventDto> Events { get; }   
        public UserInfoDto UserInfo { get; }
    }
}
