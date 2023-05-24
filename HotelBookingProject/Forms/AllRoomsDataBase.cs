using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingProject
{ 
    public partial class AllRoomsDataBase : Form
    {
        private Room room;
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public AllRoomsDataBase()
        {
            InitializeComponent();
        }

        #region Events
        private void addRoomToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RoomForm roomForm = new RoomForm();
            roomForm.ShowDialog();
        }

        private void AllRoomsDataBase_Load(object sender, EventArgs e)
        {
            LoadRooms();
        }

        private void deleteRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room");
                return;
            }
            string query = "Delete from Room where IdRoom=@idRoom;";
            var selectedRow = dgvRooms.SelectedRows[0];
            long id = Convert.ToInt64(selectedRow.Cells["IdRoom"].Value);
            if (MessageBox.Show("Are you sure?", "Warnings", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SQLiteCommand(query, connection);
                    command.Parameters.AddWithValue("@idRoom", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            
        }
        
        private void editRoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvRooms.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to edit");
                return;
            }
            var selectedRow = dgvRooms.SelectedRows[0];
            long id = Convert.ToInt64(selectedRow.Cells["IdRoom"].Value);
            room = RetrieveRoom(id);
            EditRoomForm editRoomForm = new EditRoomForm(room,id);
            //EditRoomForm editRoomForm=new EditRoomForm((Room)dgvRooms.SelectedCells[0].Tag,id);
            editRoomForm.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadRooms();
        }

        private void addRoomToolStripMenuItem1_MouseHover(object sender, EventArgs e)
        {
            statusStrip.Text = "Click to add room";
        }

        private void addRoomToolStripMenuItem1_MouseLeave(object sender, EventArgs e)
        {
            statusStrip.Text = "";
        }

        private void editRoomToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            statusStrip.Text = "Click to edit room";
        }

        private void editRoomToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            statusStrip.Text = "";
        }

        private void deleteRoomToolStripMenuItem_MouseHover(object sender, EventArgs e)
        {
            statusStrip.Text = "Click to delete room";
        }

        private void deleteRoomToolStripMenuItem_MouseLeave(object sender, EventArgs e)
        {
            statusStrip.Text = "";
        }
        #endregion

        #region Methods
        private void LoadRooms()
        {
            string query = "SELECT * FROM Room";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvRooms.DataSource = dataTable;
                connection.Close();
            }
        }
        private Room RetrieveRoom(long idRoom)
        {
            string query = "SELECT * FROM Room where IdRoom=@idRoom";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@idRoom", idRoom);

                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string roomName = (string)reader["RoomName"];
                    string description = (string)reader["Description"];
                    float price = reader.GetFloat(reader.GetOrdinal("Price"));
                    string text = (string)reader["Type"];
                    Type type = (Type)Enum.Parse(typeof(Type), text);
                    room = new Room(roomName, type, description, price);
                }
                connection.Close();
            }
            return room;
        }
        #endregion
    }
}
