using System.Collections.Generic;

namespace BookingService.DtoModels
{
    public class RouteDto
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public List<TransportDto> Transports { get; set; }
        public List<HotelDto> Hotels { get; set; }
        public List<EventDto> Events { get; set; }
        public int CountOfPersonsAdults { get; set; }
        public int CountOfPersonsChildren { get; set; }
    }
}
