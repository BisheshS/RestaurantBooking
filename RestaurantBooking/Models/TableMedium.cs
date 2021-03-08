using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Models
{
    [Serializable]
    public class TableMedium : Table
    {
        public TableMedium(string name) : base(name)
        {
            seatLimit = 5;
        }
    }
}
