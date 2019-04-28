namespace BookingService.Common
{
    public struct BookingServiceUrls
    {
        public const string HOTEL_URL = "http://localhost:6001/api/booking/";
        public const string TRANSPORT_URL = "http://localhost:6001/api/booking/";
        public const string EVENT_URL = "http://localhost:6001/api/booking/";
    }

    public struct BuyingServiceUrls
    {
        public const string HOTEL_URL = "http://localhost:6001/api/buying/hotel";
        public const string TRANSPORT_URL = "http://localhost:6001/api/buying/transport";
        public const string EVENT_URL = "http://localhost:6001/api/buying/event";
    }
}
