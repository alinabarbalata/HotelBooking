﻿using HotelBookingProject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelBookingProject
{
    public partial class ClientForm : Form
    {
        private long id;
        private const string ConnectionString = "Data Source=DatabaseHotelBooking.db";
        public ClientForm()
        {
            InitializeComponent();
        }

        #region Events
        private void ClientForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            this.Close();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void tbFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (tbFirstName.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbFirstName, "Required");
            }
        }

        private void tbFirstName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbFirstName, null);
        }

        private void tbEmail_Validating(object sender, CancelEventArgs e)
        {
            if (tbEmail.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbEmail, "Required");
            }
        }

        private void tbEmail_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbEmail, null);
        }

        private void tbLastName_Validating(object sender, CancelEventArgs e)
        {
            if (tbLastName.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbLastName, "Required");
            }
        }

        private void tbLastName_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbLastName, null);
        }

        private void tbPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            if (tbPhoneNumber.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbPhoneNumber, "Required");
            }
        }

        private void tbPhoneNumber_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError (tbPhoneNumber, null);
        }

        private void cbTermsAndConditions_Validating(object sender, CancelEventArgs e)
        {
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Not all fields were correctly completed.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            else if (!cbTermsAndConditions.Checked)
            {
                MessageBox.Show("The terms and conditions weren't checked.", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
            else
            {
                string firstName=tbFirstName.Text; 
                string lastName=tbLastName.Text;
                string email = tbEmail.Text;
                string phoneNumber = tbPhoneNumber.Text;
                DateTime birthDate = dtpBirthDate.Value;
                Client client = new Client(firstName, lastName, birthDate, email, phoneNumber);
                string username = tbUsername.Text;
                string password = tbPassword.Text;
                Account account = new Account(username, password);
                try
                {
                    addAccount(account);
                    addClient(client,id);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                MessageBox.Show("The account was succesfully created!", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                ClearForm();
                return;
            }
        }

        private void dtpBirthDate_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(dtpBirthDate, null);
        }

        private void dtpBirthDate_Validating(object sender, CancelEventArgs e)
        {
            if (dtpBirthDate.Value.AddYears(18) > DateTime.Now)
            {
                e.Cancel = true;
                errorProvider.SetError(dtpBirthDate, "Too young");
            }
        }

        private void tbUsername_Validating(object sender, CancelEventArgs e)
        {
            if (tbUsername.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbUsername, "Required");
            }
        }

        private void tbUsername_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbUsername, null);
        }

        private void tbPassword_Validating(object sender, CancelEventArgs e)
        {
            if (tbPassword.Text.Length == 0)
            {
                e.Cancel = true;
                errorProvider.SetError(tbPassword, null);
            }
        }

        private void tbPassword_Validated(object sender, EventArgs e)
        {
            errorProvider.SetError(tbPassword, null);
        }
        #endregion    

        #region Methods
        private void addClient(Client client,long idAccount)
        {
            var query = "insert into Client(IdAccount,FirstName,LastName,BirthDate,Email,PhoneNumber)" +
                        " values(@idAccount,@firstName,@lastName,@birthDate,@email,@phoneNumber);  " +
                        "SELECT last_insert_rowid()";
   
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@idAccount", idAccount);
                command.Parameters.AddWithValue("@firstName", client.FirstName);
                command.Parameters.AddWithValue("@lastName", client.LastName);
                command.Parameters.AddWithValue("@birthDate", client.BirthDate.ToShortDateString());
                command.Parameters.AddWithValue("@email", client.Email);
                command.Parameters.AddWithValue("@phoneNumber", client.PhoneNumber);

                long id = (long)command.ExecuteScalar();
                client.IdClient = id;
            }
        }

        private void addAccount(Account account)
        {
            var query = "INSERT into Account(Username,Password) values(@username,@password);SELECT last_insert_rowid()";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                var command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@username", account.Username);
                command.Parameters.AddWithValue("@password", account.Password);

                id= (long)command.ExecuteScalar();
                account.IdAccount = id;
            }
        }

        private void ClearForm()
        {
            tbLastName.Text= string.Empty;
            tbFirstName.Text= string.Empty;
            tbEmail.Text = string.Empty;
            tbUsername.Text= string.Empty;
            tbPassword.Text= string.Empty;
            tbPhoneNumber.Text = string.Empty;
            cbTermsAndConditions.Checked = false;
            dtpBirthDate.Value= DateTime.Now;
        }
        #endregion

    }
}
