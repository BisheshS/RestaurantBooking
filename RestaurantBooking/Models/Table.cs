using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Models
{
    [Serializable]
    public class Table
    {
        public string name { get; set; }
        public BookingStatus bookingStatus { get; set; }
        public int seatLimit { get; set; }

        public Table(string name)
        {
            this.name = name;
            this.bookingStatus = BookingStatus.AVAILABLE;
        }
    }
}
