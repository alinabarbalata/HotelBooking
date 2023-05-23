using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBookingProject
{
    class Account
    {
        public long IdAccount { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public Account(string username,string password)
        {
            Username = username;
            Password = password;
        }
    }
}
