using DVLD.People;
using DVLD.Users;
using DVLD_Business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Users;


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
            frmListPeople peopleForm = new frmListPeople();

            peopleForm.ShowDialog();
        }

     

        private void Form1_Load(object sender, EventArgs e)
        {

            DataTable countriesTable = clsCountry.GetAllCountries();

            cbCountries.DisplayMember = "CountryName";
            cbCountries.ValueMember = "CountryID";
            cbCountries.DataSource = countriesTable;

            dgvPeople.DataSource = clsPerson.GetAllPeople();

            DVLD.Users.frmManageUsers frm =
    new DVLD.Users.frmManageUsers();

            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
