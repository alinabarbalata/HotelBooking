using HotelBookingProject;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{
    internal class Reservation
    {
        public int IdReservation { get; }
        private Client client;
        private int IdClient { get; }
        private DateTime CheckIn { get; set; }
        private DateTime CheckOut { get; set; }
        public int Days { get; set; }
        public int TotalCost { get; }


        #region Methods
        public void ComputeReservationPeriod()
        {
            Days = (CheckOut - CheckIn).Days;
        }
        //public void ComputeTotalCost(ReservedRooms[] reservedRooms)
        //{
        //    TotalCost = 0;
        //    foreach (var room in reservedRooms)
        //    {
        //        TotalCost += room.Price;
        //    }
        //}//to modify
        #endregion
        public Reservation(Client client, DateTime checkIn, DateTime checkOut)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            IdClient = client.IdClient;
        }

    }
}