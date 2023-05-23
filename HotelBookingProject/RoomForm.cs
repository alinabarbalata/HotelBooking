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
    public partial class RoomForm : Form
    {
        public RoomForm()
        {
            InitializeComponent();
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
    }
}
