using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingProject
{ 
    public partial class AllRoomsDataBase : Form
    {
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public AllRoomsDataBase()
        {
            InitializeComponent();
        }

        private void addRoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RoomForm roomForm = new RoomForm();
            roomForm.ShowDialog();
        }

        private void AllRoomsDataBase_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Room";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query,connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvRooms.DataSource = dataTable;
            }
        }
    }
}
