using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsPerson
    {
        public int PersonID { get; set; }
        public string NationalNo { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public byte Gendor { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int NationalityCountryID { get; set; }
        public string ImagePath { get; set; }

        private clsPerson(
            int PersonID,
            string NationalNo,
            string FirstName,
            string SecondName,
            string ThirdName,
            string LastName,
            DateTime DateOfBirth,
            byte Gendor,
            string Address,
            string Phone,
            string Email,
            int NationalityCountryID,
            string ImagePath)
        {
            this.PersonID = PersonID;
            this.NationalNo = NationalNo;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
        }

        public static clsPerson Find(int PersonID)
        {
            string NationalNo = "";
            string FirstName = "";
            string SecondName = "";
            string ThirdName = "";
            string LastName = "";
            DateTime DateOfBirth = DateTime.Now;
            byte Gendor = 0;
            string Address = "";
            string Phone = "";
            string Email = "";
            int NationalityCountryID = -1;
            string ImagePath = "";

            bool isFound = clsPersonData.GetPersonInfoByID(
                PersonID,
                ref NationalNo,
                ref FirstName,
                ref SecondName,
                ref ThirdName,
                ref LastName,
                ref DateOfBirth,
                ref Gendor,
                ref Address,
                ref Phone,
                ref Email,
                ref NationalityCountryID,
                ref ImagePath);

            if (isFound)
            {
                return new clsPerson(
                    PersonID,
                    NationalNo,
                    FirstName,
                    SecondName,
                    ThirdName,
                    LastName,
                    DateOfBirth,
                    Gendor,
                    Address,
                    Phone,
                    Email,
                    NationalityCountryID,
                    ImagePath);
            }

            return null;
        }

        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
    }
}