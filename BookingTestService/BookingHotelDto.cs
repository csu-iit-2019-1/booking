using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingTestService
{
    public class BookingHotelDto
    {
        public int HotelId { get; set; }
        public int PersonId { get; set; }
        public DateTime DateDeparture { get; set; }
        public DateTime DateArrive { get; set; }
        public int CountOfPersons { get; set; }
    }
}
