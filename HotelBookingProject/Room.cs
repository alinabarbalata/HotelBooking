using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingProject
{
    internal class Room
    {
        #region Attributes
        public long IdRoom { get; set; }
        private string roomName;
        private bool isAvailable;
        public string Description { get; set; }
        public float Price { get; set; }
        private Type type;
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
            if (isAvailable == true) return "Available";
            else return "Not Available";
        }
        public void SetAvailability(bool value)
        {
            isAvailable = value;
        }
        public string GetType()
        {
            if (type == Type.Single)
                return "Single";
            else if (type == Type.Double)
                return "Double";
            else if (type == Type.Suite)
                return "Suite";
            else
                return "Deluxe";
        }
        public void SetType(Type type)
        {
            this.type = type;
        }
        #region Constructor
        
        public Room(string roomName,Type type, string description, float price)
        {
            RoomName = roomName;
            SetAvailability(true);
            Description = description;
            SetType(type);
            Price = price;
        }
        #endregion
    }
}