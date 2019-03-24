namespace BookingService.DtoModels.Booking
{
    public class BookingEventDto
    {
        public int EventId { get; set; }
        public int UserId { get; set; }
        public int CountOfPersonsAdults { get; set; }
        public int CountOfPersonsChildren { get; set; }
    }
}
