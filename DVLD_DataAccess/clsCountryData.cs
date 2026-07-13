using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace DVLD_DataAccess
{
    public class clsCountryData
    {
        public static bool GetCountryInfoByID(int CountryID, ref string CountryName)
        {
            bool isFound = false;
            SqlConnection connection =
                 new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT CountryName FROM dbo.Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                }
                else
                {
                    isFound = false;
                }

                reader.Close();

            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }


        public static bool GetCountryInfoByName(string CountryName, ref int CountryID)


        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT CountryID FROM dbo.Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    CountryID = (int)reader["CountryID"];
                }

                reader.Close();

            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }


            return isFound;
        }
        public static DataTable GetAllCountries()
        {
            DataTable countriesTable = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT CountryID, CountryName FROM dbo.Countries ORDER BY CountryName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                countriesTable.Load(reader);

                reader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return countriesTable;
        }

        public static bool GetPersonInfoByID(
    int PersonID,
    ref string NationalNo,
    ref string FirstName,
    ref string SecondName,
    ref string ThirdName,
    ref string LastName,
    ref DateTime DateOfBirth,
    ref byte Gendor,
    ref string Address,
    ref string Phone,
    ref string Email,
    ref int NationalityCountryID,
    ref string ImagePath)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"SELECT PersonID, NationalNo, FirstName, SecondName,
                 ThirdName, LastName, DateOfBirth, Gendor,
                 Address, Phone, Email, NationalityCountryID,
                 ImagePath
          FROM dbo.People
          WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (string)reader["SecondName"];

                    ThirdName = reader["ThirdName"] == DBNull.Value
                        ? ""
                        : (string)reader["ThirdName"];

                    LastName = (string)reader["LastName"];
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gendor = Convert.ToByte(reader["Gendor"]);
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];

                    Email = reader["Email"] == DBNull.Value
                        ? ""
                        : (string)reader["Email"];

                    NationalityCountryID =
                        Convert.ToInt32(reader["NationalityCountryID"]);

                    ImagePath = reader["ImagePath"] == DBNull.Value
                        ? ""
                        : (string)reader["ImagePath"];
                }

                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
    }
}
