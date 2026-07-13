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
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = clsPerson.GetAllPeople();

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

                dgvPeople.DataSource = clsPerson.GetAllPeople();
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

            dgvPeople.DataSource =
                clsPerson.GetAllPeople();
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

            dgvPeople.DataSource = clsPerson.GetAllPeople();
        }
    }
}
