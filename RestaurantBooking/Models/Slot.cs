using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace RestaurantBooking.Models
{
    public class Slot
    {
        //time and name
        public Dictionary<string, string> timeSlots;


        public Slot()
        {
            timeSlots = populateTimeSlots();
        }

        public Dictionary<string, string> getTimeSlots()
        {
            return timeSlots;
        }

        //divide time into slots
        public Dictionary<string,string> populateTimeSlots()
        {
            Dictionary<string, string> x = new Dictionary<string, string>();

            //first half
            DateTime morningSlotsStart = DateTime.ParseExact("9:30 AM","h:mm tt", CultureInfo.InvariantCulture);
            DateTime morningSlotsEnd = DateTime.ParseExact("4:30 PM", "h:mm tt", CultureInfo.InvariantCulture);

            //second half
            DateTime eveningSlotsStart = DateTime.ParseExact("7:30 PM", "h:mm tt", CultureInfo.InvariantCulture);
            DateTime eveningSlotsEnd = DateTime.ParseExact("11:30 PM", "h:mm tt", CultureInfo.InvariantCulture);

            int count = 1;
            for (DateTime i = morningSlotsStart; i < eveningSlotsEnd; i = i.AddHours(1))
            {
                if (i >= morningSlotsEnd & i < eveningSlotsStart)
                    continue;
                x.Add(i.ToString("h:mm tt"), "slot_" + count.ToString());
                count++;
            }

            return x;
        }
    }
}
