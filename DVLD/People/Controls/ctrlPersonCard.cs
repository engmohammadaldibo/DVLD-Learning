using System;
using System.IO;
using System.Windows.Forms;
using DVLD_Business;
using DVLD.People;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private clsPerson _Person;
        private int _PersonID = -1;

        public int PersonID
        {
            get
            {
                return _PersonID;
            }
        }

        public clsPerson SelectedPersonInfo
        {
            get
            {
                return _Person;
            }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();

            llEditPersonInfo.LinkClicked +=
                llEditPersonInfo_LinkClicked;

            ResetPersonInfo();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);

            if (_Person == null)
            {
                ResetPersonInfo();

                MessageBox.Show(
                    "No person with Person ID = " +
                    PersonID,
                    "Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string NationalNo)
        {
            _Person = clsPerson.Find(NationalNo);

            if (_Person == null)
            {
                ResetPersonInfo();

                MessageBox.Show(
                    "No person with National No = " +
                    NationalNo,
                    "Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            _FillPersonInfo();
        }

        private void _FillPersonInfo()
        {
            _PersonID = _Person.PersonID;

            lblPersonID.Text =
                _Person.PersonID.ToString();

            lblFullName.Text =
                _Person.FullName;

            lblNationalNo.Text =
                _Person.NationalNo;

            lblGendor.Text =
                _Person.Gendor == 0
                ? "Male"
                : "Female";

            lblEmail.Text =
                string.IsNullOrWhiteSpace(_Person.Email)
                ? "No Email"
                : _Person.Email;

            lblPhone.Text =
                _Person.Phone;

            lblDateOfBirth.Text =
                _Person.DateOfBirth
                .ToShortDateString();

            lblAddress.Text =
                _Person.Address;

            clsCountry country =
                clsCountry.Find(
                    _Person.NationalityCountryID);

            lblCountry.Text =
                country == null
                ? "Unknown"
                : country.CountryName;

            llEditPersonInfo.Enabled = true;

            _LoadPersonImage();
        }

        private void _LoadPersonImage()
        {
            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = null;

            string imagePath =
                _Person.ImagePath;

            if (!string.IsNullOrWhiteSpace(imagePath) &&
                File.Exists(imagePath))
            {
                pbPersonImage.ImageLocation =
                    imagePath;
            }
        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            _Person = null;

            lblPersonID.Text = "[????]";
            lblFullName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";

            pbPersonImage.ImageLocation = null;
            pbPersonImage.Image = null;

            llEditPersonInfo.Enabled = false;
        }

        private void llEditPersonInfo_LinkClicked(
            object sender,
            LinkLabelLinkClickedEventArgs e)
        {
            if (_PersonID == -1)
            {
                return;
            }

            frmAddUpdatePerson updatePersonForm =
                new frmAddUpdatePerson(_PersonID);

            updatePersonForm.ShowDialog();

            LoadPersonInfo(_PersonID);
        }
    }
}