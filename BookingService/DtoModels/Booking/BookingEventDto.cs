namespace BookingService.DtoModels.Booking
{
    public class BookingEventDto
    {
        public int userId { get; set; }
        public int countOfPersonsAdults { get; set; }
        public int countOfPersonsChildren { get; set; }
    }
}
