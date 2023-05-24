using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingProject
{
    public partial class AllReservationsDataBase : Form
    {
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public AllReservationsDataBase()
        {
            InitializeComponent();
        }

        #region Events
        private void AllReservationsDataBase_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM Reservation";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgvReservations.DataSource = dataTable;
                connection.Close();
            }
        }


        private void label1_MouseHover(object sender, EventArgs e)
        {
            contextMenuStrip.Show(label1, 0, label1.Height);
        }

        private void exportMenuStrip_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File | *.txt";
            saveFileDialog.Title = "Save as text file";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = File.CreateText(saveFileDialog.FileName))
                {
                    // Write column headers
                    sw.WriteLine("IdReservation\tIdClient\tCheckIn\t\t\tCheckOut\t\tDays");

                    // Write row data
                    foreach (DataGridViewRow row in dgvReservations.Rows)
                    {
                        if (!row.IsNewRow) // Skip the new row if present
                        {
                            string rowData = string.Join("\t\t", row.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value.ToString()));
                            sw.WriteLine(rowData);
                        }
                    }
                }

            }
        }

        #endregion
    }
}
