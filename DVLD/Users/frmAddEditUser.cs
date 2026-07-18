using System;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.Users
{
    public partial class frmAddEditUser : Form
    {
        private enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        private enMode _Mode;
        private int _UserID = -1;
        private clsUser _User;

        public frmAddEditUser()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public frmAddEditUser(int UserID)
        {
            InitializeComponent();

            _UserID = UserID;
            _Mode = enMode.Update;
        }

        private void _ResetDefaultValues()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtConfirmPassword.Clear();

            lblUserID.Text = "[????]";
            chkIsActive.Checked = true;

            if (_Mode == enMode.AddNew)
            {
                Text = "Add New User";
                _User = new clsUser();

                ctrlPersonCardWithFilter1.FilterEnabled(true);
                ctrlPersonCardWithFilter1.FocusFilter();
            }
            else
            {
                Text = "Update User";
                ctrlPersonCardWithFilter1.FilterEnabled(false);
            }
        }

        private void _LoadUserData()
        {
            _User = clsUser.Find(_UserID);

            if (_User == null)
            {
                MessageBox.Show(
                    "User was not found.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
                return;
            }

            ctrlPersonCardWithFilter1.LoadPersonInfo(_User.PersonID);

            lblUserID.Text = _User.UserID.ToString();
            txtUserName.Text = _User.UserName;
            txtPassword.Text = _User.Password;
            txtConfirmPassword.Text = _User.Password;
            chkIsActive.Checked = _User.IsActive;
        }

        private bool _ValidateData()
        {
            if (ctrlPersonCardWithFilter1.PersonID == -1)
            {
                MessageBox.Show(
                    "Please select a person first.",
                    "Missing Person",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show(
                    "Please enter a user name.",
                    "Missing User Name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUserName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show(
                    "Please enter a password.",
                    "Missing Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show(
                    "Password confirmation does not match.",
                    "Invalid Password",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtConfirmPassword.Focus();
                return false;
            }

            if (_Mode == enMode.AddNew &&
                clsUser.IsUserExistForPersonID(
                    ctrlPersonCardWithFilter1.PersonID))
            {
                MessageBox.Show(
                    "The selected person already has a user account.",
                    "User Already Exists",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            if (clsUser.IsUserExist(txtUserName.Text.Trim()) &&
                (_Mode == enMode.AddNew ||
                 txtUserName.Text.Trim() != _User.UserName))
            {
                MessageBox.Show(
                    "This user name is already used.",
                    "Duplicate User Name",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUserName.Focus();
                return false;
            }

            return true;
        }

        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == enMode.Update)
                _LoadUserData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_ValidateData())
                return;

            _User.PersonID =
                ctrlPersonCardWithFilter1.PersonID;

            _User.UserName =
                txtUserName.Text.Trim();

            _User.Password =
                txtPassword.Text;

            _User.IsActive =
                chkIsActive.Checked;

            if (_User.Save())
            {
                _Mode = enMode.Update;
                _UserID = _User.UserID;

                lblUserID.Text =
                    _User.UserID.ToString();

                Text = "Update User";

                ctrlPersonCardWithFilter1.FilterEnabled(false);

                MessageBox.Show(
                    "User saved successfully.",
                    "Saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "User was not saved.",
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