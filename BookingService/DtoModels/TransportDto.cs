using System.Collections.Generic;

namespace BookingService.DtoModels
{
    public class TransportDto
    {      
        public int TransportId { get; }
        public string FlightInfo { get; }
        public List<Sit> Sits {get;}
        public int StartPoint { get; }
        public int EndPoint { get; }
        public int DepartureTime { get; }
        public int ArriveTime { get; }
    }
}
