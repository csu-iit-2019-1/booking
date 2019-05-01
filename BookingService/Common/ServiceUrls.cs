namespace BookingService.Common
{
    public struct BookingServiceUrls
    {
        public const string HOTEL_URL = "http://localhost:6001/api/booking/";
        public const string TRANSPORT_URL = "http://178.128.253.228:8181/transport/booking/";
        public const string EVENT_URL = "https://iitevents.herokuapp.com/events/booking/";
    }

    public struct BuyingServiceUrls
    {
        public const string HOTEL_URL = "http://localhost:6001/api/buying/hotel";
        public const string TRANSPORT_URL = "http://178.128.253.228:8181/transport/buyout/";
        public const string EVENT_URL = "https://iitevents.herokuapp.com/events/buying";
    }
}
