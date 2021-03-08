using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Models
{
    [Serializable]
    public class TableLarge : Table
    {
        public TableLarge(string name) : base(name)
        {
            seatLimit = 10;
        }
    }
}
