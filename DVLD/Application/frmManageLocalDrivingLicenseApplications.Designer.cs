namespace DVLD.Application
{
    partial class frmManageLocalDrivingLicenseApplications
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
            this.dgvLocalApplications = new System.Windows.Forms.DataGridView();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.btnAddNewApplication = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalApplications)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLocalApplications
            // 
            this.dgvLocalApplications.AllowUserToAddRows = false;
            this.dgvLocalApplications.AllowUserToDeleteRows = false;
            this.dgvLocalApplications.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLocalApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalApplications.Location = new System.Drawing.Point(122, 104);
            this.dgvLocalApplications.MultiSelect = false;
            this.dgvLocalApplications.Name = "dgvLocalApplications";
            this.dgvLocalApplications.ReadOnly = true;
            this.dgvLocalApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLocalApplications.Size = new System.Drawing.Size(240, 150);
            this.dgvLocalApplications.TabIndex = 0;
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "Local Application ID",
            "National No",
            "Full Name",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(368, 104);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(121, 21);
            this.cbFilterBy.TabIndex = 1;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(495, 105);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(100, 20);
            this.txtFilterValue.TabIndex = 2;
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Location = new System.Drawing.Point(479, 225);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(35, 13);
            this.lblRecordsCount.TabIndex = 3;
            this.lblRecordsCount.Text = "label1";
            // 
            // btnAddNewApplication
            // 
            this.btnAddNewApplication.Location = new System.Drawing.Point(122, 260);
            this.btnAddNewApplication.Name = "btnAddNewApplication";
            this.btnAddNewApplication.Size = new System.Drawing.Size(182, 23);
            this.btnAddNewApplication.TabIndex = 4;
            this.btnAddNewApplication.Text = "Add New Application";
            this.btnAddNewApplication.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(122, 289);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmManageLocalDrivingLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 621);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnAddNewApplication);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.dgvLocalApplications);
            this.Name = "frmManageLocalDrivingLicenseApplications";
            this.Text = "frmManageLocalDrivingLicenseApplications";
            this.Load += new System.EventHandler(this.frmManageLocalDrivingLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalApplications)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLocalApplications;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Button btnAddNewApplication;
        private System.Windows.Forms.Button btnClose;
    }
}