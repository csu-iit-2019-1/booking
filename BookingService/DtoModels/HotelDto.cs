using System.Collections.Generic;

namespace BookingService.DtoModels
{
    public class HotelDto
    {
        public int HotelId;
        public string Name { get; }
        public string City { get; }
        public int Apartaments { get; }
        public double Price { get; }
        public string Description { get; }
        public double Raiting { get; }
        public List<string> PhotoUrls { get; }
        public List<string> Reviews { get; }
    }
}
