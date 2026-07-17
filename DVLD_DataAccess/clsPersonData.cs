using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPersonData
    {
        public static DataTable GetAllPeople()
        {
            DataTable peopleTable = new DataTable();

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"SELECT PersonID, NationalNo, FirstName, SecondName,
                 ThirdName, LastName, DateOfBirth, Gendor,
                 Address, Phone, Email, NationalityCountryID,
                 ImagePath
          FROM dbo.People
          ORDER BY PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                peopleTable.Load(reader);

                reader.Close();
            }
            catch (Exception)
            {
            }
            finally
            {
                connection.Close();
            }

            return peopleTable;
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

        public static int AddNewPerson(
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
            int PersonID = -1;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"INSERT INTO dbo.People
          (
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
              ImagePath
          )
          VALUES
          (
              @NationalNo,
              @FirstName,
              @SecondName,
              @ThirdName,
              @LastName,
              @DateOfBirth,
              @Gendor,
              @Address,
              @Phone,
              @Email,
              @NationalityCountryID,
              @ImagePath
          );

          SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            command.Parameters.AddWithValue(
                "@ThirdName",
                string.IsNullOrWhiteSpace(ThirdName)
                    ? (object)DBNull.Value
                    : ThirdName);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            command.Parameters.AddWithValue(
                "@Email",
                string.IsNullOrWhiteSpace(Email)
                    ? (object)DBNull.Value
                    : Email);

            command.Parameters.AddWithValue(
                "@NationalityCountryID",
                NationalityCountryID);

            command.Parameters.AddWithValue(
                "@ImagePath",
                string.IsNullOrWhiteSpace(ImagePath)
                    ? (object)DBNull.Value
                    : ImagePath);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    PersonID = Convert.ToInt32(result);
                }
            }
            catch (Exception)
            {
                PersonID = -1;
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePerson(
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
            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"UPDATE dbo.People
          SET NationalNo = @NationalNo,
              FirstName = @FirstName,
              SecondName = @SecondName,
              ThirdName = @ThirdName,
              LastName = @LastName,
              DateOfBirth = @DateOfBirth,
              Gendor = @Gendor,
              Address = @Address,
              Phone = @Phone,
              Email = @Email,
              NationalityCountryID = @NationalityCountryID,
              ImagePath = @ImagePath
          WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            command.Parameters.AddWithValue(
                "@ThirdName",
                string.IsNullOrWhiteSpace(ThirdName)
                    ? (object)DBNull.Value
                    : ThirdName);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gendor", Gendor);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            command.Parameters.AddWithValue(
                "@Email",
                string.IsNullOrWhiteSpace(Email)
                    ? (object)DBNull.Value
                    : Email);

            command.Parameters.AddWithValue(
                "@NationalityCountryID",
                NationalityCountryID);

            command.Parameters.AddWithValue(
                "@ImagePath",
                string.IsNullOrWhiteSpace(ImagePath)
                    ? (object)DBNull.Value
                    : ImagePath);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                "DELETE FROM dbo.People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                "SELECT 1 FROM dbo.People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                isFound = result != null;
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
        public static bool IsPersonExist(string NationalNo)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"SELECT 1
          FROM dbo.People
          WHERE NationalNo = @NationalNo";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@NationalNo", NationalNo);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                isFound = result != null;
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

        public static bool GetPersonInfoByNationalNo(
    string NationalNo,
    ref int PersonID,
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
                new SqlConnection(
                    clsDataAccessSettings.ConnectionString);

            string query =
                @"SELECT *
          FROM dbo.People
          WHERE NationalNo = @NationalNo";

            SqlCommand command =
                new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@NationalNo", NationalNo);

            try
            {
                connection.Open();

                SqlDataReader reader =
                    command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    PersonID =
                        (int)reader["PersonID"];

                    FirstName =
                        (string)reader["FirstName"];

                    SecondName =
                        (string)reader["SecondName"];

                    ThirdName =
                        reader["ThirdName"] == DBNull.Value
                        ? ""
                        : (string)reader["ThirdName"];

                    LastName =
                        (string)reader["LastName"];

                    DateOfBirth =
                        (DateTime)reader["DateOfBirth"];

                    Gendor =
                        (byte)reader["Gendor"];

                    Address =
                        (string)reader["Address"];

                    Phone =
                        (string)reader["Phone"];

                    Email =
                        reader["Email"] == DBNull.Value
                        ? ""
                        : (string)reader["Email"];

                    NationalityCountryID =
                        (int)reader["NationalityCountryID"];

                    ImagePath =
                        reader["ImagePath"] == DBNull.Value
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
