using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantBooking.Models
{
    public class TableMatrix
    {
        public List<Table> ogTableMatrix { get; set; }

        public TableMatrix(List<Table> tables)
        {
            //refer later

            ogTableMatrix = tables.OrderByDescending(x=>x.seatLimit).ToList();
        }

        public void addTable(Table table)
        {
            ogTableMatrix.Add(table);
        }

    }
}
