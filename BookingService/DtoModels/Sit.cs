namespace BookingService.DtoModels
{
    public class Sit
    {
        public enum ClassName { coach, comfort, business, first }
        public int SitNumber { get; set; }
        public enum State { available, notAvailable }
        public double Price { get; set; }
    }
}
