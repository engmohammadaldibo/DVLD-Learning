using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.People
{
    public partial class frmAddUpdatePerson : Form
    {
        private enum enMode
        {
            AddNew,
            Update
        }

        private enMode _Mode;
        private int _PersonID = -1;
        private clsPerson _Person;
        private string _ImagePath = "";

        public frmAddUpdatePerson()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;

            _WireEvents();
        }

        public frmAddUpdatePerson(int PersonID)
        {
            InitializeComponent();

            _Mode = enMode.Update;
            _PersonID = PersonID;

            _WireEvents();
        }

        private void _WireEvents()
        {
            Load += frmAddUpdatePerson_Load;

            btnSave.Click += btnSave_Click;
            btnClose.Click += btnClose_Click;
            btnSetImage.Click += btnSetImage_Click;
            btnRemoveImage.Click += btnRemoveImage_Click;
        }

        private void frmAddUpdatePerson_Load(
            object sender, EventArgs e)
        {
            _FillCountries();

            dtpDateOfBirth.MaxDate = DateTime.Today;

            if (_Mode == enMode.AddNew)
            {
                _PrepareAddNewMode();
            }
            else
            {
                _LoadPersonData();
            }
        }

        private void _FillCountries()
        {
            DataTable countriesTable =
                clsCountry.GetAllCountries();

            cbCountry.DataSource = countriesTable;
            cbCountry.DisplayMember = "CountryName";
            cbCountry.ValueMember = "CountryID";
        }

        private void _PrepareAddNewMode()
        {
            lblTitle.Text = "Add New Person";
            lblPersonID.Text = "N/A";

            _Person = new clsPerson();

            rbMale.Checked = true;

            dtpDateOfBirth.Value =
                DateTime.Today.AddYears(-18);

            cbCountry.SelectedValue = 169;

            if (cbCountry.SelectedIndex == -1 &&
                cbCountry.Items.Count > 0)
            {
                cbCountry.SelectedIndex = 0;
            }

            _ImagePath = "";

            pbPersonImage.Image = null;
            pbPersonImage.ImageLocation = null;

            btnRemoveImage.Enabled = false;
        }

        private void _LoadPersonData()
        {
            _Person = clsPerson.Find(_PersonID);

            if (_Person == null)
            {
                MessageBox.Show(
                    "Person was not found.",
                    "Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                Close();
                return;
            }

            lblTitle.Text = "Update Person";
            lblPersonID.Text = _Person.PersonID.ToString();

            txtNationalNo.Text = _Person.NationalNo;
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;

            dtpDateOfBirth.Value = _Person.DateOfBirth;

            if (_Person.Gender == 0)
            {
                rbMale.Checked = true;
            }
            else
            {
                rbFemale.Checked = true;
            }

            txtAddress.Text = _Person.Address;
            txtPhone.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;

            cbCountry.SelectedValue =
                _Person.NationalityCountryID;

            _ImagePath = _Person.ImagePath;

            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            pbPersonImage.Image = null;
            pbPersonImage.ImageLocation = null;

            if (!string.IsNullOrWhiteSpace(_ImagePath) &&
                File.Exists(_ImagePath))
            {
                pbPersonImage.ImageLocation = _ImagePath;
                btnRemoveImage.Enabled = true;
            }
            else
            {
                btnRemoveImage.Enabled = false;
            }
        }

        private bool _ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(
                txtNationalNo.Text))
            {
                MessageBox.Show(
                    "National number is required.");

                txtNationalNo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtFirstName.Text))
            {
                MessageBox.Show(
                    "First name is required.");

                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtSecondName.Text))
            {
                MessageBox.Show(
                    "Second name is required.");

                txtSecondName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtLastName.Text))
            {
                MessageBox.Show(
                    "Last name is required.");

                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtAddress.Text))
            {
                MessageBox.Show(
                    "Address is required.");

                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtPhone.Text))
            {
                MessageBox.Show(
                    "Phone number is required.");

                txtPhone.Focus();
                return false;
            }

            if (cbCountry.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Please select a country.");

                cbCountry.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(
                    txtEmail.Text) &&
                !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show(
                    "Please enter a valid email address.");

                txtEmail.Focus();
                return false;
            }

            return true;
        }

        private bool _NationalNoIsDuplicated()
        {
            string newNationalNo =
                txtNationalNo.Text.Trim();

            if (_Mode == enMode.AddNew)
            {
                return clsPerson.IsPersonExist(
                    newNationalNo);
            }

            bool nationalNoWasChanged =
                !string.Equals(
                    newNationalNo,
                    _Person.NationalNo,
                    StringComparison.OrdinalIgnoreCase);

            if (nationalNoWasChanged)
            {
                return clsPerson.IsPersonExist(
                    newNationalNo);
            }

            return false;
        }

        private void _ReadControlsIntoPerson()
        {
            _Person.NationalNo =
                txtNationalNo.Text.Trim();

            _Person.FirstName =
                txtFirstName.Text.Trim();

            _Person.SecondName =
                txtSecondName.Text.Trim();

            _Person.ThirdName =
                txtThirdName.Text.Trim();

            _Person.LastName =
                txtLastName.Text.Trim();

            _Person.DateOfBirth =
                dtpDateOfBirth.Value.Date;

            _Person.Gender =
                rbMale.Checked ? (byte)0 : (byte)1;

            _Person.Address =
                txtAddress.Text.Trim();

            _Person.Phone =
                txtPhone.Text.Trim();

            _Person.Email =
                txtEmail.Text.Trim();

            _Person.NationalityCountryID =
                Convert.ToInt32(
                    cbCountry.SelectedValue);

            _Person.ImagePath = _ImagePath;
        }

        private void btnSave_Click(
            object sender, EventArgs e)
        {
            if (!_ValidateInput())
            {
                return;
            }

            if (_NationalNoIsDuplicated())
            {
                MessageBox.Show(
                    "This national number is already used.",
                    "Duplicate National Number",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtNationalNo.Focus();
                return;
            }

            _ReadControlsIntoPerson();

            if (_Person.Save())
            {
                _PersonID = _Person.PersonID;
                _Mode = enMode.Update;

                lblPersonID.Text =
                    _Person.PersonID.ToString();

                lblTitle.Text = "Update Person";

                MessageBox.Show(
                    "Person saved successfully.",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Person could not be saved.",
                    "Save Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSetImage_Click(
            object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog =
                new OpenFileDialog())
            {
                openFileDialog.Title =
                    "Select Person Image";

                openFileDialog.Filter =
                    "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (openFileDialog.ShowDialog() ==
                    DialogResult.OK)
                {
                    _ImagePath =
                        openFileDialog.FileName;

                    pbPersonImage.ImageLocation =
                        _ImagePath;

                    btnRemoveImage.Enabled = true;
                }
            }
        }

        private void btnRemoveImage_Click(
            object sender, EventArgs e)
        {
            _ImagePath = "";

            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = null;

            btnRemoveImage.Enabled = false;
        }

        private void btnClose_Click(
            object sender, EventArgs e)
        {
            Close();
        }
    }
}