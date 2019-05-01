using System;

namespace BookingService.DtoModels.Booking
{
    public class BookingHotelDto
    {
        public int hotelId { get; set; }
        public int personId { get; set; }
        public DateTime dateDeparture { get; set; }
        public DateTime dateArrive { get; set; }
        public int countOfPersons { get; set; }
    }
}
