using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingProject
{
    public partial class RoomForm : Form
    {
        private readonly List<Room> _rooms;
        private string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public RoomForm()
        {
            InitializeComponent();
            _rooms = new List<Room>();
        }

        private void tbName_Validating(object sender, CancelEventArgs e)
        {
            if (tbName.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbName, "Required");
            }
        }

        private void tbName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbName, null);
        }

        private void tbPrice_Validating(object sender, CancelEventArgs e)
        {
            if (!float.TryParse(tbPrice.Text, out _)){
                e.Cancel = true;
                errorProvider.SetError(tbPrice, "Invalid price.");
            }
            else if(tbPrice.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbPrice, "Required");
            }
        }

        private void tbPrice_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbPrice, null);
        }

        private void AddComboBoxItems()
        {
            foreach(Type type  in Enum.GetValues(typeof(Type)))
            {
                //cbType.Items.Clear();
                cbType.Items.Add(type);
            }
        }

        private void RoomForm_Load(object sender, EventArgs e)
        {
            AddComboBoxItems();
        }


        private void cbType_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(cbType, null);
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("The form contains errors.","ERROR",MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                if (tbDescription.Text.Length == 0)
                {
                    DialogResult result=MessageBox.Show("The description field wasn't completed. Proceed anyway?", "WARNING",
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    { 
                        string roomName = tbName.Text;
                        string description = "-";
                        float price = float.Parse(tbPrice.Text);
                        Type type = (Type)Enum.Parse(typeof(Type), cbType.Text);
                        try
                        {
                            Room room = new Room(roomName, type, description, price);
                            addRoom(room);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        MessageBox.Show("Succesfully created a room!", "SUCCESS", MessageBoxButtons.OK,
                       MessageBoxIcon.Information);
                    }
                    else if (result == DialogResult.No)
                    {
                        //go back to completing the form
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        tbName.Text = string.Empty;
                        tbPrice.Text = string.Empty;
                        cbType.SelectedItem = null;
                    }
                }
                else
                {
                    string roomName = tbName.Text;
                    string description = tbDescription.Text;
                    float price = float.Parse(tbPrice.Text);
                    Type type = (Type)Enum.Parse(typeof(Type), cbType.SelectedItem.ToString());
                    try
                    {
                        Room room = new Room(roomName, type, description, price);
                        addRoom(room);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    MessageBox.Show("Succesfully created a room!", "SUCCESS", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void cbType_Validating(object sender, CancelEventArgs e)
        {
            if (cbType.SelectedItem == null)
            {
                e.Cancel = true;
                errorProvider.SetError(cbType, "Please select a type");
            }
        }

        private void tbDescription_Validating(object sender, CancelEventArgs e)
        {
            if (tbDescription.Text.Length == 0)
            {
                informationProvider.Show("No description entered", tbDescription);
            }
        }

        private void tbDescription_Validated(object sender, EventArgs e)
        {
            informationProvider.Hide(tbDescription);
        }

        private void addRoom(Room room)
        {
            string query = "INSERT into Room(RoomName,Description,Price,Type,Availability) VALUES(@roomName,@description,@price,@type,@availability);select last_insert_rowid();";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@roomName", room.RoomName);
                command.Parameters.AddWithValue("@description", room.Description);
                command.Parameters.AddWithValue("@price", room.Price);
                command.Parameters.AddWithValue("@type", room.GetType());
                command.Parameters.AddWithValue("@availability", room.GetAvailability());
                long id = (long)command.ExecuteScalar();
                room.IdRoom = id;
                _rooms.Add(room);
            }
        }
    }
}
