using RestaurantBooking.Models;
using System;

namespace RestaurantBooking
{
    class Program
    {
        static void Main(string[] args)
        {

            ReservationManager reservationManager = new ReservationManager();

            reservationManager.addTable(new TableSmall("table_s1"));
            reservationManager.addTable(new TableSmall("table_s2"));
            reservationManager.addTable(new TableSmall("table_s3"));
            reservationManager.addTable(new TableMedium("table_m1"));
            reservationManager.addTable(new TableMedium("table_m2"));
            reservationManager.addTable(new TableMedium("table_m3"));
            reservationManager.addTable(new TableLarge("table_l1"));
            reservationManager.addTable(new TableLarge("table_l2"));

            //Pass location of file as argument

            var fileLines = System.IO.File.ReadAllLines(args[0]);
            foreach (var singleLine in fileLines)
            {
                Console.WriteLine(singleLine);
                string[] query = singleLine.Split(" ");

                switch (query[0])
                {
                    case "book":
                        string userEmail = query[1];
                        string date = query[2];
                        string time = query[3];
                        int pax = Int32.Parse(query[4]);

                        reservationManager.createBooking(userEmail, date, time, pax);
                        break;
                    case "status_all_tables":
                        string dateFetch = query[1];
                        string slotFetch = query[2];
                        reservationManager.fetchAllTablesForSlot(dateFetch, slotFetch);
                        break;
                    case "status_user_reservation":
                        string userEmailFetch = query[1];
                        string reservationDateFetch = query[2];
                        reservationManager.fetchUserReservation(userEmailFetch, reservationDateFetch);
                        break;
                    case "status_table_reservation":
                        string tableFetch = query[1];
                        string fetchkardo = query[2];
                        reservationManager.fetchTableReservationForDate(tableFetch, fetchkardo);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
