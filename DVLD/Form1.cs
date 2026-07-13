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
using System.Data;

namespace DVLD
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvPeople.CurrentRow == null)
            {
                MessageBox.Show("Select a person first.");
                return;
            }

            int personID = Convert.ToInt32(
                dgvPeople.CurrentRow.Cells["PersonID"].Value);

            clsPerson person = clsPerson.Find(personID);

            if (person == null)
            {
                MessageBox.Show("Person not found.");
                return;
            }

            if (!person.NationalNo.StartsWith("T"))
            {
                MessageBox.Show(
                    "For safety, only test persons can be deleted.");
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Delete test person ID {personID}?",
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
                    "Delete failed. The person may be linked to other records.");
            }
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable countriesTable = clsCountry.GetAllCountries();

            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";
            cbCountries.DataSource = countriesTable;

            dgvPeople.DataSource = clsPerson.GetAllPeople();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
