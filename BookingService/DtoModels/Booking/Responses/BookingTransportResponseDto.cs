namespace BookingService.DtoModels.Booking.Response
{
    public class BookingTransportResponseDto
    {
        public int BookingId { get; }
        public int TransportId { get; }
        public int PersonId { get; }
        public int CountOfAdults { get; }
        public int CountOfKids { get; }
        public double Price { get; }
    }
}
