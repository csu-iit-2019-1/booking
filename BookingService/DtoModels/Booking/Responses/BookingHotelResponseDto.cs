namespace BookingService.DtoModels.Booking.Response
{
    public class BookingHotelResponseDto
    {
        public int BookingId { get; }
        public int PersonId { get; }
        public int HotelId { get; }
        public int StayTime { get; }
        public int CountOfPersons { get; }
        public string Status { get; }
    }
}
