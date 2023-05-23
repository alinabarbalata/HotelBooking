using HotelBookingProject;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingProject
{
    internal class Reservation
    {
        public long IdReservation { get; set; }
        private Client client;
        private long IdClient { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
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
        public Reservation(long idClient, DateTime checkIn, DateTime checkOut)
        {
            CheckIn = checkIn;
            CheckOut = checkOut;
            IdClient = idClient;
        }

    }
}