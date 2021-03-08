using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantBooking.Models
{
    [Serializable]
    public class TableSmall : Table
    {
        public TableSmall(string name) : base(name)
        {
            seatLimit = 2;
        }
    }
}
