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
    public partial class AllClientsDataBase : Form
    {
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public AllClientsDataBase()
        {
            InitializeComponent();
        }

        private void AllClientsDataBase_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM CLIENT";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvClients.DataSource = dataTable;
            }
        }
    }
}
