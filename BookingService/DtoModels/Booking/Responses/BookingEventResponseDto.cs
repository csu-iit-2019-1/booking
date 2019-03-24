namespace BookingService.DtoModels.Booking.Response
{
    public class BookingEventResponseDto
    {
        public int BookingId { get; }
        public int UserId { get; }
        public int EventId { get; }
        public int CountOfPersonsAdults { get; }
        public int CountOfPersonsChildren { get; }
        public string Status { get; }
    }
}
