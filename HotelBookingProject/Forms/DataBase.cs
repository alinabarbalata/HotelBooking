using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingProject
{
    public partial class DataBase : Form
    {
        public DataBase()
        {
            InitializeComponent();
        }

        #region Events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            RoomForm roomForm = new RoomForm();
            roomForm.ShowDialog();
        }

        private void btnReservations_Click(object sender, EventArgs e)
        {
            AllReservationsDataBase allReservationsDataBase = new AllReservationsDataBase();
            allReservationsDataBase.ShowDialog();
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            AllRoomsDataBase allRoomsDataBase = new AllRoomsDataBase();
            allRoomsDataBase.ShowDialog();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            AllClientsDataBase allClientsDataBase = new AllClientsDataBase();
            allClientsDataBase.ShowDialog();
        }
#endregion
    }
}
