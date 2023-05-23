using HotelBookingProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingProject
{
    internal class ReservedRooms
    {
        public long IdReservedRooms { get; set; }
        public long IdReservation { get; set; }
        public long IdRoom { get; set; }

        public ReservedRooms(Reservation reservation ,long idRoom)
        {
            IdReservation = reservation.IdReservation;
            IdRoom = idRoom;
        }
    }
}
