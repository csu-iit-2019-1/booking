using System;
using System.Collections.Generic;

namespace BookingService.DtoModels
{
    public class HotelDto
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int Apartaments { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public double Raiting { get; set; }
        public List<string> PhotoUrls { get; set; }
        public List<string> Reviews { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateArrive { get; set; }
    }
}
