using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{
    internal class Room
    {
        #region Attributes
        public int IdRoom { get; }
        private string roomName;
        private bool isAvailable;
        public string Description { get; set; }
        public float Price { get; set; }
        public Type Type { get; set; }
        #endregion

        public string RoomName
        {
            get
            {
                return roomName;
            }
            set
            {
                StringBuilder sb = new StringBuilder("Room ");
                sb.Append(value);
                roomName = sb.ToString();
            }
        }
        public string GetAvailability()
        {
            if (isAvailable == true) return "Yes";
            else return "No";
        }
        public void SetAvailability(bool value)
        {
            isAvailable = value;
        }


        #region Constructor
        public Room(string roomName, Type type, string description, float price)
        {
            RoomName = roomName;
            SetAvailability(true);
            Description = description;
            Type = type;
            Price = price;
        }
        #endregion
    }
}