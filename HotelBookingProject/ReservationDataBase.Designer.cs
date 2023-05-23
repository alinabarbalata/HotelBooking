
namespace HotelBookingProject
{
    partial class ReservationDataBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvReservationsClient = new System.Windows.Forms.DataGridView();
            this.btnAddReservation = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservationsClient)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvReservationsClient
            // 
            this.dgvReservationsClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvReservationsClient.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReservationsClient.Location = new System.Drawing.Point(18, 114);
            this.dgvReservationsClient.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dgvReservationsClient.Name = "dgvReservationsClient";
            this.dgvReservationsClient.RowHeadersWidth = 62;
            this.dgvReservationsClient.Size = new System.Drawing.Size(1164, 560);
            this.dgvReservationsClient.TabIndex = 0;
            // 
            // btnAddReservation
            // 
            this.btnAddReservation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddReservation.Location = new System.Drawing.Point(472, 18);
            this.btnAddReservation.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddReservation.Name = "btnAddReservation";
            this.btnAddReservation.Size = new System.Drawing.Size(249, 68);
            this.btnAddReservation.TabIndex = 1;
            this.btnAddReservation.Text = "Add Reservation";
            this.btnAddReservation.UseVisualStyleBackColor = true;
            this.btnAddReservation.Click += new System.EventHandler(this.btnAddReservation_Click);
            // 
            // ReservationDataBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.btnAddReservation);
            this.Controls.Add(this.dgvReservationsClient);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ReservationDataBase";
            this.Text = "ReservationDataBase";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReservationDataBase_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReservationDataBase_FormClosed);
            this.Load += new System.EventHandler(this.ReservationDataBase_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReservationsClient)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvReservationsClient;
        private System.Windows.Forms.Button btnAddReservation;
    }
}