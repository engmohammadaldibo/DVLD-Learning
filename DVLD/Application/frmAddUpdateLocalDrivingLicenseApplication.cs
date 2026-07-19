using System;
using System.Data;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.Application
{
    public partial class frmAddUpdateLocalDrivingLicenseApplication : Form
    {
        private clsLocalDrivingLicenseApplication
            _localDrivingLicenseApplication;

        // مؤقتًا حتى ننتهي من تسجيل الدخول.
        // يجب أن يكون المستخدم رقم 1 موجودًا في جدول Users.
        private const int TemporaryCreatedByUserID = 1;

        public frmAddUpdateLocalDrivingLicenseApplication()
        {
            InitializeComponent();

            Load += frmAddUpdateLocalDrivingLicenseApplication_Load;
            btnClose.Click += btnClose_Click;
            btnSave.Click += btnSave_Click;
        }

        private void frmAddUpdateLocalDrivingLicenseApplication_Load(
            object sender,
            EventArgs e)
        {
            _localDrivingLicenseApplication =
                new clsLocalDrivingLicenseApplication();

            lblLocalDrivingLicenseApplicationID.Text = "N/A";

            lblApplicationDate.Text =
                DateTime.Now.ToShortDateString();

            lblApplicationFees.Text = "0";

            lblCreatedByUser.Text =
                TemporaryCreatedByUserID.ToString();

            _FillLicenseClasses();
        }

        private void _FillLicenseClasses()
        {
            DataTable dtLicenseClasses =
                clsLicenseClass.GetAllLicenseClasses();

            cbLicenseClass.DataSource = dtLicenseClasses;
            cbLicenseClass.DisplayMember = "ClassName";
            cbLicenseClass.ValueMember = "LicenseClassID";

            cbLicenseClass.DropDownStyle =
                ComboBoxStyle.DropDownList;

            if (cbLicenseClass.Items.Count > 0)
            {
                cbLicenseClass.SelectedIndex = 0;
            }
        }

        private bool _ValidateInput()
        {
            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show(
                    "Please select a person first.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (cbLicenseClass.SelectedIndex == -1 ||
                cbLicenseClass.SelectedValue == null)
            {
                MessageBox.Show(
                    "Please select a license class.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_ValidateInput())
            {
                return;
            }

            _localDrivingLicenseApplication.ApplicantPersonID =
                ctrlPersonCardWithFilter1.PersonID;

            _localDrivingLicenseApplication.ApplicationDate =
                DateTime.Now;

            _localDrivingLicenseApplication.ApplicationTypeID = 1;

            _localDrivingLicenseApplication.ApplicationStatus =
                clsApplication.enApplicationStatus.New;

            _localDrivingLicenseApplication.LastStatusDate =
                DateTime.Now;

            _localDrivingLicenseApplication.PaidFees = 0;

            _localDrivingLicenseApplication.CreatedByUserID =
                TemporaryCreatedByUserID;

            _localDrivingLicenseApplication.LicenseClassID =
                Convert.ToInt32(cbLicenseClass.SelectedValue);

            if (_localDrivingLicenseApplication.Save())
            {
                lblLocalDrivingLicenseApplicationID.Text =
                    _localDrivingLicenseApplication
                        .LocalDrivingLicenseApplicationID
                        .ToString();

                MessageBox.Show(
                    "Application saved successfully.",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show(
                    "Failed to save the application.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}