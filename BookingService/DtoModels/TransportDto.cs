using System.Collections.Generic;

namespace BookingService.DtoModels
{
    public class TransportDto
    {      
        public int TransportId { get; set; }
        public string FlightInfo { get; set; }
        public List<Sit> Sits { get; set; }
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
        public int DepartureTime { get; set; }
        public int ArriveTime { get; set; }
    }
}
