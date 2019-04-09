using BookingService.Models;
using System.Collections.Generic;
using Xunit;


namespace BookingTests
{
    public class BookingDbUnitTest
    {
        [Fact]
        public void TestFindAfterAdding()
        {
            var db = new BookingDB();
            (int, BookingType) key = (1, BookingType.Event);
            List<int> values = new List<int>()
            {
                101,102,103
            };

            db.AddOrUpdate(key, values);
            var returnedValues = db.Find(key);
            Assert.Equal(values, returnedValues);
        }

        [Fact]
        public void TestFindAfterUpdating()
        {
            var db = new BookingDB();
            (int, BookingType) key = (1, BookingType.Event);
            List<int> oldValues = new List<int>()
            {
                101,102,103
            };
            List<int> newValues = new List<int>()
            {
                201,202,203
            };

            db.AddOrUpdate(key, oldValues);
            db.AddOrUpdate(key, newValues);  

            var returnedValues = db.Find(key);
            Assert.Equal(newValues, returnedValues);
        }

        [Fact]
        public void TestFindNull()
        {
            var db = new BookingDB();
            (int, BookingType) key = (1, BookingType.Event);
            var returnedValues = db.Find(key);
            Assert.Null(returnedValues);
        }

        [Fact]
        public void TestDbIsEmpty()
        {
            var db = new BookingDB();            
            Assert.True(db.IsEmpty());
        }

        [Fact]
        public void TestDbIsNotEmpty()
        {
            var db = new BookingDB();
            (int, BookingType) key = (1, BookingType.Event);
            List<int> values = new List<int>()
            {
                101,102,103
            };
            db.AddOrUpdate(key, values);

            Assert.False(db.IsEmpty());
        }

        [Fact]
        public void TestRemove()
        {
            var db = new BookingDB();
            (int, BookingType) key = (1, BookingType.Event);
            List<int> values = new List<int>()
            {
                101,102,103
            };

            db.AddOrUpdate(key, values);
            db.Remove(key);

            Assert.Null(db.Find(key));
        }
    }
}
