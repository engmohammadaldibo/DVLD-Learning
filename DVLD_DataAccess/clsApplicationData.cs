using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsApplicationData
    {
        public static bool GetApplicationInfoByID(
            int applicationID,
            ref int applicantPersonID,
            ref DateTime applicationDate,
            ref int applicationTypeID,
            ref byte applicationStatus,
            ref DateTime lastStatusDate,
            ref float paidFees,
            ref int createdByUserID)
        {
            bool isFound = false;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT *
                    FROM Applications
                    WHERE ApplicationID = @ApplicationID;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicationID", applicationID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader =
                               command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                applicantPersonID =
                                    Convert.ToInt32(
                                        reader["ApplicantPersonID"]);

                                applicationDate =
                                    Convert.ToDateTime(
                                        reader["ApplicationDate"]);

                                applicationTypeID =
                                    Convert.ToInt32(
                                        reader["ApplicationTypeID"]);

                                applicationStatus =
                                    Convert.ToByte(
                                        reader["ApplicationStatus"]);

                                lastStatusDate =
                                    Convert.ToDateTime(
                                        reader["LastStatusDate"]);

                                paidFees =
                                    Convert.ToSingle(
                                        reader["PaidFees"]);

                                createdByUserID =
                                    Convert.ToInt32(
                                        reader["CreatedByUserID"]);
                            }
                        }
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dtApplications = new DataTable();

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT *
                    FROM Applications
                    ORDER BY ApplicationDate DESC;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader =
                               command.ExecuteReader())
                        {
                            dtApplications.Load(reader);
                        }
                    }
                    catch
                    {
                        return dtApplications;
                    }
                }
            }

            return dtApplications;
        }

        public static int AddNewApplication(
            int applicantPersonID,
            DateTime applicationDate,
            int applicationTypeID,
            byte applicationStatus,
            DateTime lastStatusDate,
            float paidFees,
            int createdByUserID)
        {
            int applicationID = -1;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    INSERT INTO Applications
                    (
                        ApplicantPersonID,
                        ApplicationDate,
                        ApplicationTypeID,
                        ApplicationStatus,
                        LastStatusDate,
                        PaidFees,
                        CreatedByUserID
                    )
                    VALUES
                    (
                        @ApplicantPersonID,
                        @ApplicationDate,
                        @ApplicationTypeID,
                        @ApplicationStatus,
                        @LastStatusDate,
                        @PaidFees,
                        @CreatedByUserID
                    );

                    SELECT SCOPE_IDENTITY();";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicantPersonID", applicantPersonID);

                    command.Parameters.AddWithValue(
                        "@ApplicationDate", applicationDate);

                    command.Parameters.AddWithValue(
                        "@ApplicationTypeID", applicationTypeID);

                    command.Parameters.AddWithValue(
                        "@ApplicationStatus", applicationStatus);

                    command.Parameters.AddWithValue(
                        "@LastStatusDate", lastStatusDate);

                    command.Parameters.AddWithValue(
                        "@PaidFees", paidFees);

                    command.Parameters.AddWithValue(
                        "@CreatedByUserID", createdByUserID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null &&
                            int.TryParse(result.ToString(),
                                out int insertedID))
                        {
                            applicationID = insertedID;
                        }
                    }
                    catch
                    {
                        applicationID = -1;
                    }
                }
            }

            return applicationID;
        }

        public static bool UpdateApplication(
            int applicationID,
            int applicantPersonID,
            DateTime applicationDate,
            int applicationTypeID,
            byte applicationStatus,
            DateTime lastStatusDate,
            float paidFees,
            int createdByUserID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    UPDATE Applications
                    SET
                        ApplicantPersonID = @ApplicantPersonID,
                        ApplicationDate = @ApplicationDate,
                        ApplicationTypeID = @ApplicationTypeID,
                        ApplicationStatus = @ApplicationStatus,
                        LastStatusDate = @LastStatusDate,
                        PaidFees = @PaidFees,
                        CreatedByUserID = @CreatedByUserID
                    WHERE ApplicationID = @ApplicationID;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicationID", applicationID);

                    command.Parameters.AddWithValue(
                        "@ApplicantPersonID", applicantPersonID);

                    command.Parameters.AddWithValue(
                        "@ApplicationDate", applicationDate);

                    command.Parameters.AddWithValue(
                        "@ApplicationTypeID", applicationTypeID);

                    command.Parameters.AddWithValue(
                        "@ApplicationStatus", applicationStatus);

                    command.Parameters.AddWithValue(
                        "@LastStatusDate", lastStatusDate);

                    command.Parameters.AddWithValue(
                        "@PaidFees", paidFees);

                    command.Parameters.AddWithValue(
                        "@CreatedByUserID", createdByUserID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        rowsAffected = 0;
                    }
                }
            }

            return rowsAffected > 0;
        }

        public static bool DeleteApplication(int applicationID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    DELETE FROM Applications
                    WHERE ApplicationID = @ApplicationID;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicationID", applicationID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        rowsAffected = 0;
                    }
                }
            }

            return rowsAffected > 0;
        }

        public static bool IsApplicationExist(int applicationID)
        {
            bool isFound = false;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT 1
                    FROM Applications
                    WHERE ApplicationID = @ApplicationID;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicationID", applicationID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();
                        isFound = result != null;
                    }
                    catch
                    {
                        isFound = false;
                    }
                }
            }

            return isFound;
        }

        public static int GetActiveApplicationID(
            int personID,
            int applicationTypeID)
        {
            int activeApplicationID = -1;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT ApplicationID
                    FROM Applications
                    WHERE ApplicantPersonID = @ApplicantPersonID
                      AND ApplicationTypeID = @ApplicationTypeID
                      AND ApplicationStatus = 1;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicantPersonID", personID);

                    command.Parameters.AddWithValue(
                        "@ApplicationTypeID", applicationTypeID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null &&
                            int.TryParse(result.ToString(),
                                out int applicationID))
                        {
                            activeApplicationID = applicationID;
                        }
                    }
                    catch
                    {
                        activeApplicationID = -1;
                    }
                }
            }

            return activeApplicationID;
        }

        public static bool DoesPersonHaveActiveApplication(
            int personID,
            int applicationTypeID)
        {
            return GetActiveApplicationID(
                personID, applicationTypeID) != -1;
        }

        public static int GetActiveApplicationIDForLicenseClass(
            int personID,
            int applicationTypeID,
            int licenseClassID)
        {
            int activeApplicationID = -1;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    SELECT Applications.ApplicationID
                    FROM Applications
                    INNER JOIN LocalDrivingLicenseApplications
                        ON Applications.ApplicationID =
                           LocalDrivingLicenseApplications.ApplicationID
                    WHERE Applications.ApplicantPersonID =
                          @ApplicantPersonID
                      AND Applications.ApplicationTypeID =
                          @ApplicationTypeID
                      AND LocalDrivingLicenseApplications.LicenseClassID =
                          @LicenseClassID
                      AND Applications.ApplicationStatus = 1;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicantPersonID", personID);

                    command.Parameters.AddWithValue(
                        "@ApplicationTypeID", applicationTypeID);

                    command.Parameters.AddWithValue(
                        "@LicenseClassID", licenseClassID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null &&
                            int.TryParse(result.ToString(),
                                out int applicationID))
                        {
                            activeApplicationID = applicationID;
                        }
                    }
                    catch
                    {
                        activeApplicationID = -1;
                    }
                }
            }

            return activeApplicationID;
        }

        public static bool UpdateStatus(
            int applicationID,
            short newStatus)
        {
            int rowsAffected = 0;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"
                    UPDATE Applications
                    SET
                        ApplicationStatus = @NewStatus,
                        LastStatusDate = @LastStatusDate
                    WHERE ApplicationID = @ApplicationID;";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@ApplicationID", applicationID);

                    command.Parameters.AddWithValue(
                        "@NewStatus", newStatus);

                    command.Parameters.AddWithValue(
                        "@LastStatusDate", DateTime.Now);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        rowsAffected = 0;
                    }
                }
            }

            return rowsAffected > 0;
        }
    }
}