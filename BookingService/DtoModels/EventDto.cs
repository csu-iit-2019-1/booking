using System;

namespace BookingService.DtoModels
{
    public class EventDto
    {
        public int EventId { get; }
        public string Name { get; }
        public string City { get; }
        public double Price { get; }
        public string Description { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public int FreeSpace { get; }
    }
}
