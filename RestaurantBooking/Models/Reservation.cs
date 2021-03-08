using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Models
{
    public class Reservation
    {
        public string date;
        public string slot;
        public Table table;
        public User user;

        public Reservation(string date, string slot, Table table, User user)
        {
            this.date = date;
            this.slot = slot;
            this.table = table;
            this.user = user;
        }
    }
}
