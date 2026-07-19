using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLocalDrivingLicenseApplicationData
    {
        public static bool GetLocalDrivingLicenseApplicationInfoByID(
            int localDrivingLicenseApplicationID,
            ref int applicationID,
            ref int licenseClassID)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"SELECT *
                  FROM LocalDrivingLicenseApplications
                  WHERE LocalDrivingLicenseApplicationID =
                        @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@LocalDrivingLicenseApplicationID",
                localDrivingLicenseApplicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    applicationID =
                        (int)reader["ApplicationID"];

                    licenseClassID =
                        (int)reader["LicenseClassID"];
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(
            int applicationID,
            ref int localDrivingLicenseApplicationID,
            ref int licenseClassID)
        {
            bool isFound = false;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"SELECT *
                  FROM LocalDrivingLicenseApplications
                  WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@ApplicationID",
                applicationID);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;

                    localDrivingLicenseApplicationID =
                        (int)reader["LocalDrivingLicenseApplicationID"];

                    licenseClassID =
                        (int)reader["LicenseClassID"];
                }

                reader.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"SELECT *
                  FROM LocalDrivingLicenseApplications_View
                  ORDER BY ApplicationDate DESC";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }

        public static int AddNewLocalDrivingLicenseApplication(
            int applicationID,
            int licenseClassID)
        {
            int localDrivingLicenseApplicationID = -1;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"INSERT INTO LocalDrivingLicenseApplications
                    (ApplicationID, LicenseClassID)
                  VALUES
                    (@ApplicationID, @LicenseClassID);

                  SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@ApplicationID",
                applicationID);

            command.Parameters.AddWithValue(
                "@LicenseClassID",
                licenseClassID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null &&
                    int.TryParse(result.ToString(), out int insertedID))
                {
                    localDrivingLicenseApplicationID = insertedID;
                }
            }
            catch
            {
                localDrivingLicenseApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }

            return localDrivingLicenseApplicationID;
        }

        public static bool UpdateLocalDrivingLicenseApplication(
            int localDrivingLicenseApplicationID,
            int applicationID,
            int licenseClassID)
        {
            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"UPDATE LocalDrivingLicenseApplications
                  SET ApplicationID = @ApplicationID,
                      LicenseClassID = @LicenseClassID
                  WHERE LocalDrivingLicenseApplicationID =
                        @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@LocalDrivingLicenseApplicationID",
                localDrivingLicenseApplicationID);

            command.Parameters.AddWithValue(
                "@ApplicationID",
                applicationID);

            command.Parameters.AddWithValue(
                "@LicenseClassID",
                licenseClassID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }

        public static bool DeleteLocalDrivingLicenseApplication(
            int localDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query =
                @"DELETE FROM LocalDrivingLicenseApplications
                  WHERE LocalDrivingLicenseApplicationID =
                        @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue(
                "@LocalDrivingLicenseApplicationID",
                localDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}