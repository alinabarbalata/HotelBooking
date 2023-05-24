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
    public partial class ReservationDataBase : Form
    {
        private long IdClient;
        private string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public ReservationDataBase(long idClient)
        {
            IdClient = idClient;
            InitializeComponent();
        }

        #region Events
        private void btnAddReservation_Click(object sender, EventArgs e)
        {
            ReservationForm reservationForm = new ReservationForm(IdClient);
            reservationForm.ShowDialog();
        }

        private void ReservationDataBase_Load(object sender, EventArgs e)
        {
            LoadReservations();
        }

        private void ReservationDataBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            this.Close();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(label1, 0, label1.Height);
        }

        private void dgvReservationsClient_MouseHover(object sender, EventArgs e)
        {
            contextMenuStrip1.Hide();
        }

        private void addReservationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReservationForm reservationForm = new ReservationForm(IdClient);
            reservationForm.ShowDialog();
        }
        #endregion

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadReservations();
        }

        #region Methods
        private void LoadReservations()
        {
            var query = "SELECT CheckIn,CheckOut,Days FROM Reservation where IdClient=@idClient;";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                var command = new SQLiteCommand(query, connection);
                connection.Open();
                command.Parameters.AddWithValue("@idClient", IdClient);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvReservationsClient.DataSource = dataTable;
            }
        }
        #endregion
    }
}
