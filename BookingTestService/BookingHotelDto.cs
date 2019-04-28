using System;

namespace BookingTestService
{
    public class BookingHotelDto
    {
        public int HotelId { get; set; }
        public int PersonId { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateArrive { get; set; }
        public int CountOfPersons { get; set; }
    }
}
