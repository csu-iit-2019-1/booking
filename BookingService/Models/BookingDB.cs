using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingService.Models
{
    public class BookingDB
    {
        private ConcurrentDictionary<(int personId, BookingType type), List<int>> database = new ConcurrentDictionary<(int, BookingType), List<int>>();

        public List<int> Find((int, BookingType) key)
        {
            return database.TryGetValue(key, out List<int> bookingIds) ? bookingIds : null;
        }

        public void AddOrUpdate((int, BookingType) key, List<int> value)
        {
            database.AddOrUpdate
                (key, value,
                (index, oldValue) => value);
        }

        public bool IsEmpty()
        {
            return database.IsEmpty;
        }

        public void Remove((int, BookingType) key)
        {
            database.TryRemove(key, out List<int> items);
        }
    }
}
