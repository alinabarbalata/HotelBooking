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
    public partial class ReservationDataBase : Form
    {
        public ReservationDataBase()
        {
            InitializeComponent();
        }

        private void btnAddReservation_Click(object sender, EventArgs e)
        {
            ReservationForm reservationForm = new ReservationForm();
            reservationForm.ShowDialog();
        }

        private void ReservationDataBase_Load(object sender, EventArgs e)
        {
        }

        private void ReservationDataBase_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            this.Close();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void ReservationDataBase_FormClosing(object sender, FormClosingEventArgs e)
        {
        }
    }
}
