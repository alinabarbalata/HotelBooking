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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HotelBookingProject
{
    public partial class Form1 : Form
    {
        private long IdClient;
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            IdClient = RetrieveClientId();
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
            else if(VerifySignIn()==true)
            {
                ReservationDataBase reservationDataBase = new ReservationDataBase(IdClient);
                this.Hide();
                reservationDataBase.ShowDialog();
            }
            else if (VerifySignIn() == false)
            {
                MessageBox.Show("Incorrect username or password", "Trouble signing in", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            ClientForm formClient = new ClientForm();
            this.Hide();
            formClient.ShowDialog();
        }

        private long RetrieveClientId()
        {
            long id = 0;
            string query = "SELECT c.IdClient FROM Client c JOIN Account a ON c.IdAccount = a.IdAccount WHERE a.Username = @username;";
            using (SQLiteConnection connection=new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@username", tbUsername.Text);
                id = Convert.ToInt32(command.ExecuteScalar());
            }
            return id;
        }
        private bool VerifySignIn()
        {
            string username = tbUsername.Text;
            string password= tbPassword.Text;
            string query = "SELECT COUNT(*) from Account where Username=@username AND Password=@password";
            using(SQLiteConnection connection=new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                int count=Convert.ToInt32(command.ExecuteScalar());
                if (count > 0)
                    return true;
                else return false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}
