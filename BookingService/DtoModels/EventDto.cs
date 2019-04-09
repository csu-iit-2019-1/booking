using System;

namespace BookingService.DtoModels
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FreeSpace { get; set; }
    }
}
