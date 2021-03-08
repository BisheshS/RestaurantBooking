using RestaurantBooking.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace RestaurantBooking
{
    public class ReservationManager
    {
        public List<Reservation> reservations;

        public Dictionary<Time, TableMatrix> tablu;
        
        public List<Table> tables;

        public Slot slot;

        public ReservationManager()
        {
            tablu = new Dictionary<Time, TableMatrix>();

            tables = new List<Table>();

            reservations = new List<Reservation>();

            slot = new Slot();
            slot.populateTimeSlots();

            slot.timeSlots = slot.timeSlots.ToDictionary(k => k.Key.ToLower(),k=>k.Value);
            slot.timeSlots= slot.timeSlots.ToDictionary(k => k.Key.Replace(" ",""),k=>k.Value);

        }

        public void createBooking(string emailId, string date, string time, int pax) 
        {
            //validate booking request 
            //TODO for invalid time and extra people and invalid date

            Time bookingTime = new Time(slot.timeSlots[time], date);

            var exists = new KeyValuePair<Time,TableMatrix>();
            if (!tablu.Any(k => k.Key.slot == slot.timeSlots[time] && k.Key.date == date))
            {
                List<Table> tempTables = Utils.DeepClone(tables);
                tablu.Add(bookingTime, new TableMatrix(tempTables));
                exists = tablu.First(k => k.Key.slot == slot.timeSlots[time] && k.Key.date == date);
            }
            else
            {
                exists = tablu.First(k => k.Key.slot == slot.timeSlots[time] && k.Key.date == date);
            }

            bool successFactor = false;

            while (pax > 0 )
            {
                AllotSeats(exists, time, emailId, date, ref pax, ref successFactor);
                if(successFactor == false) { 
                    Console.WriteLine("Sorry, we failed to reserve tables for " + emailId + " " + date + 
                        " " + bookingTime.slot + ". Reason: No tables available"  );
                    break;
                }
            }
        }

        public void AllotSeats(KeyValuePair<Time, TableMatrix> exists, string time, string emailId ,string date, ref int pax, ref bool success)
        {
            success = false;
            foreach (var tableAtThisTime in exists.Value.ogTableMatrix)
            {
                if (tableAtThisTime.bookingStatus == BookingStatus.AVAILABLE)
                {
                    if (tableAtThisTime.seatLimit == pax)
                    {
                        tableAtThisTime.bookingStatus = BookingStatus.UNAVAILABLE;
                        User user = new User(emailId, date, time, tableAtThisTime.seatLimit);
                        reservations.Add(new Reservation(date, slot.timeSlots[time], tableAtThisTime, user));
                        Console.WriteLine("Successfully reserved " + tableAtThisTime.name + " for " + emailId + " " + date + " " +
                            slot.timeSlots[time] + " for " + pax + " persons");
                        pax = pax - tableAtThisTime.seatLimit;
                        success = true;
                        return;
                    }
                    else if (tableAtThisTime.seatLimit < pax)
                    {
                        tableAtThisTime.bookingStatus = BookingStatus.UNAVAILABLE;
                        User user = new User(emailId, date, time, tableAtThisTime.seatLimit);
                        reservations.Add(new Reservation(date, slot.timeSlots[time], tableAtThisTime, user));
                        Console.WriteLine("Successfully reserved " + tableAtThisTime.name + " for " + emailId + " " + date + " " +
                            slot.timeSlots[time] + " for " + tableAtThisTime.seatLimit + " persons");
                        pax = pax - tableAtThisTime.seatLimit;
                        success = true;
                    }
                }
            }

        }

        public void addTable(Table table)
        {
            tables.Add(table);
        }

        public void fetchAllTablesForSlot(string date, string timeSlot)
        {
            Time bookingTime = new Time(timeSlot, date);

            var exists = new KeyValuePair<Time, TableMatrix>();
            if (!tablu.Any(k => k.Key.slot == timeSlot && k.Key.date == date))
            {
                List<Table> tempTables = Utils.DeepClone(tables);
                tablu.Add(bookingTime, new TableMatrix(tempTables));
                exists = tablu.First(k => k.Key.slot == timeSlot && k.Key.date == date);
            }
            else
            {
                exists = tablu.First(k => k.Key.slot == timeSlot && k.Key.date == date);
            }

            //PRINT PART
            Console.WriteLine("All Table Status for " + timeSlot + " " + date);
            foreach (var item in exists.Value.ogTableMatrix)
            {
                bool availability = false;
                foreach (var reservation in reservations)
                {
                    if (reservation.table.name == item.name && reservation.slot == timeSlot 
                        && reservation.user.date == date)
                    {
                        availability = true;
                        Console.WriteLine(item.name + " " + reservation.user.pax + "persons " + reservation.user.emailId );
                    }
                }
                if (availability==false)
                {
                    Console.WriteLine(item.name + " " + "<available>");
                }
            }
        }

        public void fetchUserReservation(string email, string date)
        {
            List<string> tableNames = new List<string>(); ;
            foreach (var item in reservations)
            {
                if (item.user.emailId == email && item.date == date)
                {
                    tableNames.Add(item.table.name);
                    
                }
            }
            if (tableNames.Count == 0)
            {
                Console.WriteLine("No reservation for " + email + " " + "on " + date);
            }
            else
            {
                Console.Write("User reservation for " + email + " " + "on " + date + ": ");
                for (int i = 0; i < tableNames.Count-1; i++)
                {
                    Console.Write(tableNames[i] + ", ");
                }
                Console.WriteLine(tableNames[tableNames.Count-1]);
            }
        }

        public void fetchTableReservationForDate(string tableName, string date)
        {
            List<string> slotNames = slot.timeSlots.Values.ToList();
            foreach (var eachSlot in slotNames)
            {
                bool success = false;
                foreach (var reservation in reservations)
                {
                    if (reservation.slot == eachSlot && reservation.date == date && reservation.table.name == tableName)
                    {
                        success = true;
                        Console.WriteLine(eachSlot + " " + reservation.user.pax + "persons " + reservation.user.emailId);
                    }                    
                }
                if (success == false)
                {
                    Console.WriteLine(eachSlot + " <available>");
                }
            }               

        }
    }
}
