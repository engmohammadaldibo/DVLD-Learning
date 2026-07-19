using System;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Please enter User Name.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtUserName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter Password.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                txtPassword.Focus();
                return;
            }

            clsUser User = clsUser.Find(txtUserName.Text.Trim(), txtPassword.Text);

            if (User == null)
            {
                MessageBox.Show("Invalid User Name or Password.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            if (!User.IsActive)
            {
                MessageBox.Show("Your account is inactive.",
                    "Login Failed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);

                return;
            }

            clsGlobal.CurrentUser = User;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}