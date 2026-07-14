using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Business;

namespace DVLD.People
{
    public partial class frmListPeople : Form
    {

        private DataTable _PeopleTable;

        public frmListPeople()
        {
            InitializeComponent();

            cbFilterBy.SelectedIndexChanged += cbFilterBy_SelectedIndexChanged;

            txtFilterValue.TextChanged += txtFilterValue_TextChanged;
        }

        private void frmListPeople_Load(
    object sender, EventArgs e)
        {
            cbFilterBy.Items.Clear();

            cbFilterBy.Items.AddRange(
                new object[]
                {
            "None",
            "PersonID",
            "NationalNo",
            "FirstName",
            "SecondName",
            "ThirdName",
            "LastName",
            "Phone",
            "Email"
                });

            cbFilterBy.SelectedIndex = 0;

            txtFilterValue.Visible = false;

            _PreparePeopleGrid();
            _RefreshPeopleList();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDeletePerson_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow == null)
            {
                MessageBox.Show("Select a person first.");
                return;
            }

            int personID = Convert.ToInt32(
                dgvPeople.CurrentRow.Cells["PersonID"].Value);

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to delete Person ID {personID}?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.No)
            {
                return;
            }

            if (clsPerson.DeletePerson(personID))
            {
                MessageBox.Show("Person deleted successfully.");

                _RefreshPeopleList();
            }
            else
            {
                MessageBox.Show(
                    "Delete failed. The person may be linked to users, " +
                    "drivers, applications, or licenses.");
            }
        }

        private void btnAddPerson_Click(
    object sender, EventArgs e)
        {
            frmAddUpdatePerson addPersonForm =
                new frmAddUpdatePerson();

            addPersonForm.ShowDialog();

            _RefreshPeopleList();
        }

        private void btnEditPerson_Click(
    object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow == null)
            {
                MessageBox.Show(
                    "Select a person first.");

                return;
            }

            int personID = Convert.ToInt32(dgvPeople.CurrentRow.Cells["PersonID"].Value);

            frmAddUpdatePerson updatePersonForm = new frmAddUpdatePerson(personID);

            updatePersonForm.ShowDialog();

            _RefreshPeopleList();
        }

        private void _PreparePeopleGrid()
        {
            dgvPeople.ReadOnly = true;
            dgvPeople.AllowUserToAddRows = false;
            dgvPeople.AllowUserToDeleteRows = false;
            dgvPeople.MultiSelect = false;

            dgvPeople.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvPeople.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void _RefreshPeopleList()
        {
            _PeopleTable = clsPerson.GetAllPeople();

            dgvPeople.DataSource = _PeopleTable;

            _ApplyFilter();
        }

        private void _ApplyFilter()
        {
            if (_PeopleTable == null)
            {
                return;
            }

            string filterBy = cbFilterBy.Text;
            string filterValue =
                txtFilterValue.Text.Trim();

            if (filterBy == "None" ||
                string.IsNullOrWhiteSpace(filterValue))
            {
                _PeopleTable.DefaultView.RowFilter = "";

                _UpdateRecordsCount();
                return;
            }

            if (filterBy == "PersonID")
            {
                int personID;

                if (int.TryParse(filterValue, out personID))
                {
                    _PeopleTable.DefaultView.RowFilter =
                        "PersonID = " + personID;
                }
                else
                {
                    _PeopleTable.DefaultView.RowFilter =
                        "1 = 0";
                }
            }
            else
            {
                filterValue =
                    filterValue.Replace("'", "''");

                _PeopleTable.DefaultView.RowFilter =
                    "[" + filterBy + "] LIKE '%" +
                    filterValue + "%'";
            }

            _UpdateRecordsCount();
        }

        private void _UpdateRecordsCount()
        {
            if (_PeopleTable == null)
            {
                lblRecordsCount.Text = "0";
                return;
            }

            lblRecordsCount.Text =
                _PeopleTable.DefaultView.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            bool filterIsEnabled =
                cbFilterBy.Text != "None";

            txtFilterValue.Visible =
                filterIsEnabled;

            txtFilterValue.Clear();

            if (filterIsEnabled)
            {
                txtFilterValue.Focus();
            }

            _ApplyFilter();
        }

        private void txtFilterValue_TextChanged(
            object sender, EventArgs e)
        {
            _ApplyFilter();
        }
    }
}
