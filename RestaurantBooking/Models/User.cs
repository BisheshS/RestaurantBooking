using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Models
{
    public class User
    {
        public string emailId { get; set; }
        public string date { get; set; }
        public string startTime { get; set; }
        public int pax { get; set; }

        public User(string emailId, string date, string startTime, int pax)
        {
            this.emailId = emailId;
            this.date = date;
            this.startTime = startTime;
            this.pax = pax;
        }

    }
}
