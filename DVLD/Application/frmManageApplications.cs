using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.Application
{
    public partial class frmManageApplications : Form
    {
        private static DataTable _dtAllApplications;

        public frmManageApplications()
        {
            InitializeComponent();

            Load += frmManageApplications_Load;
            cbFilterBy.SelectedIndexChanged +=
                cbFilterBy_SelectedIndexChanged;

            txtFilterValue.TextChanged +=
                txtFilterValue_TextChanged;
        }

        private void frmManageApplications_Load(
            object sender,
            EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Visible = false;

            _RefreshApplicationsList();
        }

        private void _RefreshApplicationsList()
        {
            _dtAllApplications =
                clsApplication.GetAllApplications();

            dgvApplications.DataSource =
                _dtAllApplications;

            _FormatApplicationsGrid();
            _UpdateRecordsCount();
        }

        private void _FormatApplicationsGrid()
        {
            if (dgvApplications.Columns.Count == 0)
                return;

            dgvApplications.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvApplications.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvApplications.MultiSelect = false;
            dgvApplications.ReadOnly = true;
            dgvApplications.AllowUserToAddRows = false;
            dgvApplications.AllowUserToDeleteRows = false;
            dgvApplications.RowHeadersVisible = false;
        }

        private void _UpdateRecordsCount()
        {
            lblRecordsCount.Text =
                dgvApplications.Rows.Count.ToString();
        }

        private void _OpenAddNewApplicationForm()
        {
            using (
                frmAddUpdateLocalDrivingLicenseApplication frm =
                    new frmAddUpdateLocalDrivingLicenseApplication())
            {
                frm.ShowDialog();
            }

            _RefreshApplicationsList();
        }

        private void btnAddNewApplication_Click(
            object sender,
            EventArgs e)
        {
            _OpenAddNewApplicationForm();
        }

        private void btnAddApplication_Click(
            object sender,
            EventArgs e)
        {
            _OpenAddNewApplicationForm();
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
            if (_dtAllApplications == null)
                return;

            string filterColumn =
                _GetSelectedFilterColumn();

            string filterValue =
                txtFilterValue.Text
                    .Trim()
                    .Replace("'", "''");

            if (string.IsNullOrEmpty(filterColumn) ||
                string.IsNullOrWhiteSpace(filterValue))
            {
                _dtAllApplications.DefaultView.RowFilter =
                    string.Empty;

                _UpdateRecordsCount();
                return;
            }

            switch (filterColumn)
            {
                case "ApplicationID":
                case "ApplicantPersonID":
                case "ApplicationTypeID":
                case "ApplicationStatus":
                    _ApplyNumericFilter(
                        filterColumn,
                        filterValue);
                    break;
            }

            _UpdateRecordsCount();
        }

        private string _GetSelectedFilterColumn()
        {
            switch (cbFilterBy.Text)
            {
                case "Application ID":
                    return "ApplicationID";

                case "Applicant Person ID":
                    return "ApplicantPersonID";

                case "Application Type ID":
                    return "ApplicationTypeID";

                case "Status":
                    return "ApplicationStatus";

                default:
                    return string.Empty;
            }
        }

        private void _ApplyNumericFilter(
            string columnName,
            string filterValue)
        {
            if (int.TryParse(
                filterValue,
                out int numericValue))
            {
                _dtAllApplications.DefaultView.RowFilter =
                    $"{columnName} = {numericValue}";
            }
            else
            {
                _dtAllApplications.DefaultView.RowFilter =
                    $"{columnName} = -1";
            }
        }
    }
}