using HotelBooking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingProject
{
    internal class ReservedRooms
    {
        public int IdReservedRooms { get; }
        public int IdReservation { get; set; }
        public int IdRoom { get; set; }

        ReservedRooms(Reservation reservation ,Room room)
        {
            IdReservation = reservation.IdReservation;
            IdRoom = room.IdRoom;
        }
    }
}
