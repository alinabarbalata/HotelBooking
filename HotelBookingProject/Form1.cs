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
    public partial class Form1 : Form
    {
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            if(tbUsername.Text=="admin" )
            {
                if (tbPassword.Text == "admin")
                {
                    DataBase dataBase = new DataBase();
                    this.Hide();
                    dataBase.ShowDialog();
                }
                else
                    MessageBox.Show("Incorrect password","Trouble signing in",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                ReservationDataBase reservationDataBase = new ReservationDataBase();
                this.Hide();
                reservationDataBase.ShowDialog();
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            ClientForm formClient = new ClientForm();
            this.Hide();
            formClient.ShowDialog();
        }
    }
}
