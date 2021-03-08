using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Models
{
    public class Time
    {
        public string slot;

        public string date;


        public Time(string slot, string date)
        {
            this.slot = slot;
            this.date = date;
        }

    }
}
