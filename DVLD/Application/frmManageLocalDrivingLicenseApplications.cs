using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.Application
{
    public partial class frmManageLocalDrivingLicenseApplications : Form
    {
        private DataTable _dtLocalApplications;

        public frmManageLocalDrivingLicenseApplications()
        {
            InitializeComponent();

            Load += frmManageLocalDrivingLicenseApplications_Load;
            cbFilterBy.SelectedIndexChanged +=
                cbFilterBy_SelectedIndexChanged;

            txtFilterValue.TextChanged +=
                txtFilterValue_TextChanged;

            btnAddNewApplication.Click +=
                btnAddNewApplication_Click;

            btnClose.Click += btnClose_Click;
        }

        private void frmManageLocalDrivingLicenseApplications_Load(
            object sender,
            EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;

            _RefreshList();
        }

        private void _RefreshList()
        {
            _dtLocalApplications =
                clsLocalDrivingLicenseApplication
                    .GetAllLocalDrivingLicenseApplications();

            dgvLocalApplications.DataSource =
                _dtLocalApplications;

            dgvLocalApplications.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvLocalApplications.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvLocalApplications.MultiSelect = false;
            dgvLocalApplications.ReadOnly = true;
            dgvLocalApplications.AllowUserToAddRows = false;

            lblRecordsCount.Text =
                dgvLocalApplications.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            txtFilterValue.Clear();

            txtFilterValue.Visible =
                cbFilterBy.Text != "None";

            if (txtFilterValue.Visible)
                txtFilterValue.Focus();

            _ApplyFilter();
        }

        private void txtFilterValue_TextChanged(
            object sender,
            EventArgs e)
        {
            _ApplyFilter();
        }

        private void _ApplyFilter()
        {
            if (_dtLocalApplications == null)
                return;

            if (cbFilterBy.Text == "None" ||
                string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                _dtLocalApplications.DefaultView.RowFilter =
                    "";

                lblRecordsCount.Text =
                    dgvLocalApplications.Rows.Count.ToString();

                return;
            }

            string value =
                txtFilterValue.Text
                    .Trim()
                    .Replace("'", "''");

            switch (cbFilterBy.Text)
            {
                case "Local Application ID":

                    if (int.TryParse(value, out int id))
                    {
                        _dtLocalApplications.DefaultView.RowFilter =
                            $"LocalDrivingLicenseApplicationID = {id}";
                    }
                    else
                    {
                        _dtLocalApplications.DefaultView.RowFilter =
                            "LocalDrivingLicenseApplicationID = -1";
                    }

                    break;

                case "National No":

                    _dtLocalApplications.DefaultView.RowFilter =
                        $"NationalNo LIKE '{value}%'";

                    break;

                case "Full Name":

                    _dtLocalApplications.DefaultView.RowFilter =
                        $"FullName LIKE '{value}%'";

                    break;

                case "Status":

                    _dtLocalApplications.DefaultView.RowFilter =
                        $"Status LIKE '{value}%'";

                    break;
            }

            lblRecordsCount.Text =
                dgvLocalApplications.Rows.Count.ToString();
        }

        private void btnAddNewApplication_Click(
            object sender,
            EventArgs e)
        {
            using (
                frmAddUpdateLocalDrivingLicenseApplication frm =
                new frmAddUpdateLocalDrivingLicenseApplication())
            {
                frm.ShowDialog();
            }

            _RefreshList();
        }

        private void btnClose_Click(
            object sender,
            EventArgs e)
        {
            Close();
        }
    }
}