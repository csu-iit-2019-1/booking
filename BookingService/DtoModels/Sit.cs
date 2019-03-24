namespace BookingService.DtoModels
{
    public class Sit
    {
        public enum ClassName { coach, comfort, business, first }
        public int SitNumber { get; }
        public enum State { available, notAvailable }
        public double Price { get; }
    }
}
