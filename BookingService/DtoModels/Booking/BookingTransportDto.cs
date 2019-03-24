namespace BookingService.DtoModels.Booking
{
    public class BookingTransportDto
    {
        public int TransportId { get; set; }
        public int PersonId { get; set; }
        public int CountOfAdults { get; set; }
        public int CountOfKids { get; set; }
    }
}
