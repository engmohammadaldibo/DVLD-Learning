using System;
using System.Windows.Forms;
using DVLD_Business;
using DVLD.Users;

namespace DVLD.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {
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
                return ctrlPersonCard1.SelectedPersonInfo;
            }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        private void ctrlPersonCardWithFilter_Load(
            object sender,
            EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void _FindNow()
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text))
            {
                MessageBox.Show(
                    "Please enter a value to search.",
                    "Missing Value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtFilterValue.Focus();
                return;
            }

            if (cbFilterBy.Text == "Person ID")
            {
                int PersonID;

                if (!int.TryParse(txtFilterValue.Text, out PersonID))
                {
                    MessageBox.Show(
                        "Person ID must be a number.",
                        "Invalid Person ID",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    txtFilterValue.Focus();
                    return;
                }

                ctrlPersonCard1.LoadPersonInfo(PersonID);
            }
            else
            {
                ctrlPersonCard1.LoadPersonInfo(
                    txtFilterValue.Text.Trim());
            }

            _PersonID = ctrlPersonCard1.PersonID;

            if (_PersonID == -1)
            {
                MessageBox.Show(
                    "Person was not found.",
                    "Not Found",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnFind_Click(
            object sender,
            EventArgs e)
        {
            _FindNow();
        }

        private void txtFilterValue_KeyPress(
            object sender,
            KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
            {
                e.Handled =
                    !char.IsControl(e.KeyChar) &&
                    !char.IsDigit(e.KeyChar);
            }
        }

        private void txtFilterValue_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                _FindNow();
            }
        }

        private void cbFilterBy_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            txtFilterValue.Clear();
            txtFilterValue.Focus();
        }

        private void btnAddNewPerson_Click(
            object sender,
            EventArgs e)
        {
            frmAddUpdatePerson frm =
                new frmAddUpdatePerson();

            frm.ShowDialog();
        }

        public void LoadPersonInfo(int PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Text = PersonID.ToString();

            ctrlPersonCard1.LoadPersonInfo(PersonID);

            _PersonID = ctrlPersonCard1.PersonID;
        }

        public void LoadPersonInfo(string NationalNo)
        {
            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = NationalNo;

            ctrlPersonCard1.LoadPersonInfo(NationalNo);

            _PersonID = ctrlPersonCard1.PersonID;
        }

        public void FilterEnabled(bool Enable)
        {
            gbFilter.Enabled = Enable;
        }

        public void FocusFilter()
        {
            txtFilterValue.Focus();
        }
    }
}