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
            clsPerson person = clsPerson.Find(1);

            if (person != null)
            {
                string fullName =
                    $"{person.FirstName} {person.SecondName} " +
                    $"{person.ThirdName} {person.LastName}";

                MessageBox.Show(
                    $"Person ID: {person.PersonID}\n" +
                    $"National No: {person.NationalNo}\n" +
                    $"Full Name: {fullName}");
            }
            else
            {
                MessageBox.Show("Person not found.");
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
