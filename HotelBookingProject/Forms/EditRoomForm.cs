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
    public partial class EditRoomForm : Form
    {
        private string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        private readonly Room _room;
        private long _idRoom;
        public EditRoomForm(Room room,long idRoom)
        {
            InitializeComponent();
            _room = room;
            _idRoom = idRoom;
        }

        #region Events
        private void EditRoomForm_Load(object sender, EventArgs e)
        {
            tbName.Text = _room.RoomName;
            tbDescription.Text = _room.Description;
            tbPrice.Text = _room.Price.ToString();
            AddComboBoxItems();
            cbType.Text= _room.GetType();
        }

        //private void btnEditRoom_Click(object sender, EventArgs e)
        //{
        //    _room.RoomName = tbName.Text;
        //    _room.Description = tbDescription.Text;
        //    _room.Price = float.Parse(tbPrice.Text);
        //    _room.SetType((Type)Enum.Parse(typeof(Type), cbType.SelectedItem.ToString()));
        //    try
        //    {
        //        MessageBox.Show(" " + _room.RoomName);
        //        updateRoom(_room, _idRoom);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    MessageBox.Show("Succesfully edited a room!", "SUCCESS", MessageBoxButtons.OK,
        //                MessageBoxIcon.Information);
        //    this.Close();
        //}
        #endregion

        #region Methods
        private void updateRoom(Room room,long idRoom)
        {
            string query = "UPDATE Room SET RoomName=@roomName where IdRoom=@idRoom;";
            using(SQLiteConnection connection=new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@roomName", room.RoomName);
                command.Parameters.AddWithValue("@idRoom", idRoom);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        private void AddComboBoxItems()
        {
            foreach (Type type in Enum.GetValues(typeof(Type)))
            {
                cbType.Items.Add(type);
            }
        }
        #endregion
    }
}
