using DVLD.People;
using DVLD_Business;
using System;
using System.Data;
using System.Windows.Forms;

namespace DVLD.Users
{
    public partial class frmManageUsers : Form
    {
        private static DataTable _dtAllUsers;

        public frmManageUsers()
        {
            InitializeComponent();
        }

        private void _RefreshUsersList()
        {
            _dtAllUsers = clsUser.GetAllUsers();
            dgvUsers.DataSource = _dtAllUsers;

            lblRecordsCount.Text = dgvUsers.Rows.Count.ToString();
        }

        private void _FormatUsersGrid()
        {
            if (dgvUsers.Columns.Count < 5)
                return;

            dgvUsers.Columns[0].HeaderText = "User ID";
            dgvUsers.Columns[1].HeaderText = "Person ID";
            dgvUsers.Columns[2].HeaderText = "Full Name";
            dgvUsers.Columns[3].HeaderText = "User Name";
            dgvUsers.Columns[4].HeaderText = "Is Active";

            dgvUsers.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvUsers.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
        }

        private int _GetSelectedUserID()
        {
            if (dgvUsers.CurrentRow == null)
                return -1;

            return Convert.ToInt32(
                dgvUsers.CurrentRow.Cells[0].Value);
        }

        private void frmManageUsers_Load(object sender, EventArgs e)
        {
            _RefreshUsersList();
            _FormatUsersGrid();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
                return;

            int personID = Convert.ToInt32(
                dgvUsers.CurrentRow.Cells[1].Value);

            frmShowPersonInfo frm =
                new frmShowPersonInfo(personID);

            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();

            frm.ShowDialog();

            _RefreshUsersList();

        }


        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = _GetSelectedUserID();

            if (userID == -1)
                return;

            frmAddEditUser frm = new frmAddEditUser(userID);

            frm.ShowDialog();

            _RefreshUsersList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int userID = _GetSelectedUserID();

            if (userID == -1)
                return;

            if (MessageBox.Show("Are you sure you want to delete this user?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (clsUser.DeleteUser(userID))
            {
                MessageBox.Show("User deleted successfully.",
                    "Deleted",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                _RefreshUsersList();
            }
            else
            {
                MessageBox.Show("User was not deleted.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();

            frm.ShowDialog();

            _RefreshUsersList();
        }
    }
}
