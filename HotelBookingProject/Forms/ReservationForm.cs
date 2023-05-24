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
    public partial class ReservationForm : Form
    {
        private long IdClient;
        private Client client;
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public ReservationForm(long idClient)
        {
            IdClient = idClient;
            InitializeComponent();
            PopulateCheckedListBox();
        }

        #region Events
        private void dtpCheckOut_Validating(object sender, CancelEventArgs e)
        {
            if(dtpCheckOut.Value<dtpCheckIn.Value)
            { 
                e.Cancel = true;
                errorProvider.SetError(dtpCheckOut, "Check Out has to be later than Check In");
            }
        }

        private void dtpCheckOut_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(dtpCheckOut, null);
        }

        private void btnReserve_Click(object sender, EventArgs e)
        {
            DateTime checkIn = dtpCheckIn.Value;
            DateTime checkOut = dtpCheckOut.Value;
            try
            {
                Reservation reservation = new Reservation(IdClient, checkIn, checkOut);
                AddReservation(reservation);
                //AddReservedRooms(reservation);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Reservation made with success!", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            this.Close();
        }

        private void clbRooms_Validating(object sender, CancelEventArgs e)
        {
            if (clbRooms.CheckedItems.Count == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(clbRooms, "Check at least one item");
            }
        }

        private void clbRooms_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(clbRooms, null);
        }

        private void dtpCheckIn_Validating(object sender, CancelEventArgs e)
        {
            if (dtpCheckIn.Value < DateTime.Now)
            {
                e.Cancel = true;
                errorProvider.SetError(dtpCheckIn, "Invalid Check In Date");
            }
        }

        private void dtpCheckIn_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(dtpCheckIn, null);
        }
        #endregion

        #region Methods
        public void PopulateCheckedListBox()
        {
            string query = "SELECT IdRoom,RoomName FROM Room WHERE Availability='Available'";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                clbRooms.DataSource = dataTable;
                clbRooms.DisplayMember = "RoomName";
                clbRooms.ValueMember = "IdRoom";
            }
        }
        private void AddReservation(Reservation reservation)
        {
            string query = "Insert into reservation(IdClient,CheckIn,CheckOut,Days)" +
                "Values(@idClient,@checkIn,@checkOut,@days);Select last_insert_rowid();";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@idClient", IdClient);
                command.Parameters.AddWithValue("@checkIn", reservation.CheckIn.ToShortDateString());
                command.Parameters.AddWithValue("@checkOut", reservation.CheckOut.ToShortDateString());
                command.Parameters.AddWithValue("@days", reservation.GetDays());

                long id = (long)command.ExecuteScalar();
                reservation.IdReservation = id;
            }

        }
        //private void AddReservedRooms(Reservation reservation)
        //{
        //    string insertQuery = "INSERT into ReservedRooms(IdReservation,IdRoom) values(@idReservation,@idRoom);";
        //    string selectQuery = "Select last_insert_rowid();";
        //    using (SQLiteConnection connection=new SQLiteConnection(ConnectionString))
        //    {
        //        connection.Open();
        //        var insertCommand = new SQLiteCommand(insertQuery, connection);
        //        var selectCommand = new SQLiteCommand(selectQuery, connection);
        //        foreach(var item in checkedValues)
        //        {
        //            ReservedRooms reservedRooms = new ReservedRooms(reservation,item);
        //            insertCommand.Parameters.AddWithValue("@idReservation", reservation.IdReservation);
        //            insertCommand.Parameters.AddWithValue("@idRoom",item);
        //            //insertCommand.ExecuteNonQuery();
        //            long id=(long)selectCommand.ExecuteScalar();
        //            reservedRooms.IdReservedRooms = id;
        //            _reservedRooms.Add(reservedRooms);
        //            insertCommand.Parameters.Clear();
        //        }
        //    }
        //}
        #endregion
    }
}
