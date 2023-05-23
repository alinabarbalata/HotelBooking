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
        private readonly List<Reservation> _reservations;
        private readonly List<ReservedRooms> _reservedRooms;
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public ReservationForm(long idClient)
        {
            IdClient = idClient;
            InitializeComponent();
            _reservations= new List<Reservation>();
            _reservedRooms = new List<ReservedRooms>();
            PopulateCheckedListBox();
        }

        public void PopulateCheckedListBox()
        {
            string query = "SELECT IdRoom,RoomName FROM Room WHERE Availability='Available'";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter=new SQLiteDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                clbRooms.DataSource = dataTable;
                clbRooms.DisplayMember = "RoomName";
                clbRooms.ValueMember = "IdRoom";
                //SQLiteCommand command = new SQLiteCommand(query, connection);
                //using (SQLiteDataReader reader = command.ExecuteReader())
                //{
                //    clbRooms.Items.Clear();
                //    while (reader.Read())
                //    {
                //        string roomName = reader.GetString(0);
                //        clbRooms.Items.Add(roomName);
                //    }
                //}
            }
        }

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
            //Client client = RetrieveClient(IdClient);
            DateTime checkIn = dtpCheckIn.Value;
            DateTime checkOut = dtpCheckOut.Value;
            try
            {
                Reservation reservation = new Reservation(IdClient, checkIn, checkOut);
                AddReservation(reservation);
                AddReservedRooms(reservation);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddReservation(Reservation reservation)
        {
            string query = "Insert into reservation(IdClient,CheckIn,CheckOut,Days)" +
                "Values(@idClient,@checkIn,@checkOut,@days);Select last_insert_rowid();";
            using(SQLiteConnection connection=new SQLiteConnection(ConnectionString))
            {
                connection.Open();  
                var command= new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@idClient", IdClient);
                command.Parameters.AddWithValue("@checkIn", reservation.CheckIn);
                command.Parameters.AddWithValue("@checkOut", reservation.CheckOut);
                command.Parameters.AddWithValue("@days", reservation.Days);

                long id = (long)command.ExecuteScalar();
                reservation.IdReservation = id;
                _reservations.Add(reservation); 
            }

        }
        private void AddReservedRooms(Reservation reservation)
        {
            string query = "INSERT into ReservedRooms(IdReservation,IdRoom) values(@idReservation,@idRoom);Select last_insert_rowid();";
            using (SQLiteConnection connection=new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                foreach(long item in clbRooms.CheckedItems)
                {
                    ReservedRooms reservedRooms = new ReservedRooms(reservation, item);
                    command.Parameters.AddWithValue("@idReservation", reservation.IdReservation);
                    command.Parameters.AddWithValue("@idRoom", item);
                    command.ExecuteNonQuery();
                    long id=(long)command.ExecuteScalar();
                    reservedRooms.IdReservedRooms = id;
                    _reservedRooms.Add(reservedRooms);
                    command.Parameters.Clear();
                }
            }
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

        private Client RetrieveClient(long idClient)
        {
            string query = "SELECT* FROM CLIENT where IdClient=@idClient";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@idClient",idClient );

                SQLiteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    string firstName = (string)reader["FirstName"];
                    string lastName = (string)reader["LastName"];
                    DateTime birthDate = (DateTime)reader["BirthDate"];
                    string email = (string)reader["Email"];
                    string phoneNumber = (string)reader["PhoneNumber"];
                    client = new Client(firstName, lastName, birthDate, email, phoneNumber);
                    
                }
            }
            return client;
        }
    }
}
