using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {
        public static bool GetTestAppointmentInfoByID(
            int testAppointmentID,
            ref int testTypeID,
            ref int localDrivingLicenseApplicationID,
            ref DateTime appointmentDate,
            ref decimal paidFees,
            ref int createdByUserID,
            ref bool isLocked,
            ref int retakeTestApplicationID)
        {
            bool isFound = false;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query =
                    @"SELECT *
                      FROM TestAppointments
                      WHERE TestAppointmentID = @TestAppointmentID";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TestAppointmentID",
                        testAppointmentID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader =
                               command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                isFound = true;

                                testTypeID =
                                    Convert.ToInt32(reader["TestTypeID"]);

                                localDrivingLicenseApplicationID =
                                    Convert.ToInt32(
                                        reader[
                                            "LocalDrivingLicenseApplicationID"]);

                                appointmentDate =
                                    Convert.ToDateTime(
                                        reader["AppointmentDate"]);

                                paidFees =
                                    Convert.ToDecimal(reader["PaidFees"]);

                                createdByUserID =
                                    Convert.ToInt32(
                                        reader["CreatedByUserID"]);

                                isLocked =
                                    Convert.ToBoolean(reader["IsLocked"]);

                                retakeTestApplicationID =
                                    reader["RetakeTestApplicationID"] ==
                                    DBNull.Value
                                        ? -1
                                        : Convert.ToInt32(
                                            reader[
                                                "RetakeTestApplicationID"]);
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

        public static int AddNewTestAppointment(
            int testTypeID,
            int localDrivingLicenseApplicationID,
            DateTime appointmentDate,
            decimal paidFees,
            int createdByUserID,
            bool isLocked,
            int retakeTestApplicationID)
        {
            int testAppointmentID = -1;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query =
                    @"INSERT INTO TestAppointments
                      (
                          TestTypeID,
                          LocalDrivingLicenseApplicationID,
                          AppointmentDate,
                          PaidFees,
                          CreatedByUserID,
                          IsLocked,
                          RetakeTestApplicationID
                      )
                      VALUES
                      (
                          @TestTypeID,
                          @LocalDrivingLicenseApplicationID,
                          @AppointmentDate,
                          @PaidFees,
                          @CreatedByUserID,
                          @IsLocked,
                          @RetakeTestApplicationID
                      );

                      SELECT SCOPE_IDENTITY();";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TestTypeID",
                        testTypeID);

                    command.Parameters.AddWithValue(
                        "@LocalDrivingLicenseApplicationID",
                        localDrivingLicenseApplicationID);

                    command.Parameters.AddWithValue(
                        "@AppointmentDate",
                        appointmentDate);

                    command.Parameters.AddWithValue(
                        "@PaidFees",
                        paidFees);

                    command.Parameters.AddWithValue(
                        "@CreatedByUserID",
                        createdByUserID);

                    command.Parameters.AddWithValue(
                        "@IsLocked",
                        isLocked);

                    command.Parameters.AddWithValue(
                        "@RetakeTestApplicationID",
                        retakeTestApplicationID == -1
                            ? (object)DBNull.Value
                            : retakeTestApplicationID);

                    try
                    {
                        connection.Open();

                        object result = command.ExecuteScalar();

                        if (result != null &&
                            int.TryParse(
                                result.ToString(),
                                out int insertedID))
                        {
                            testAppointmentID = insertedID;
                        }
                    }
                    catch
                    {
                        testAppointmentID = -1;
                    }
                }
            }

            return testAppointmentID;
        }

        public static bool UpdateTestAppointment(
            int testAppointmentID,
            int testTypeID,
            int localDrivingLicenseApplicationID,
            DateTime appointmentDate,
            decimal paidFees,
            int createdByUserID,
            bool isLocked,
            int retakeTestApplicationID)
        {
            int rowsAffected;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query =
                    @"UPDATE TestAppointments
                      SET TestTypeID =
                              @TestTypeID,
                          LocalDrivingLicenseApplicationID =
                              @LocalDrivingLicenseApplicationID,
                          AppointmentDate =
                              @AppointmentDate,
                          PaidFees =
                              @PaidFees,
                          CreatedByUserID =
                              @CreatedByUserID,
                          IsLocked =
                              @IsLocked,
                          RetakeTestApplicationID =
                              @RetakeTestApplicationID
                      WHERE TestAppointmentID =
                            @TestAppointmentID";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TestAppointmentID",
                        testAppointmentID);

                    command.Parameters.AddWithValue(
                        "@TestTypeID",
                        testTypeID);

                    command.Parameters.AddWithValue(
                        "@LocalDrivingLicenseApplicationID",
                        localDrivingLicenseApplicationID);

                    command.Parameters.AddWithValue(
                        "@AppointmentDate",
                        appointmentDate);

                    command.Parameters.AddWithValue(
                        "@PaidFees",
                        paidFees);

                    command.Parameters.AddWithValue(
                        "@CreatedByUserID",
                        createdByUserID);

                    command.Parameters.AddWithValue(
                        "@IsLocked",
                        isLocked);

                    command.Parameters.AddWithValue(
                        "@RetakeTestApplicationID",
                        retakeTestApplicationID == -1
                            ? (object)DBNull.Value
                            : retakeTestApplicationID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return rowsAffected > 0;
        }

        public static DataTable GetTestAppointmentsByLocalApplicationID(
            int localDrivingLicenseApplicationID,
            int testTypeID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query =
                    @"SELECT
                          TestAppointmentID,
                          AppointmentDate,
                          PaidFees,
                          IsLocked
                      FROM TestAppointments
                      WHERE LocalDrivingLicenseApplicationID =
                            @LocalDrivingLicenseApplicationID
                        AND TestTypeID =
                            @TestTypeID
                      ORDER BY AppointmentDate DESC";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@LocalDrivingLicenseApplicationID",
                        localDrivingLicenseApplicationID);

                    command.Parameters.AddWithValue(
                        "@TestTypeID",
                        testTypeID);

                    try
                    {
                        connection.Open();

                        using (SqlDataReader reader =
                               command.ExecuteReader())
                        {
                            dt.Load(reader);
                        }
                    }
                    catch
                    {
                        dt.Clear();
                    }
                }
            }

            return dt;
        }

        public static bool LockTestAppointment(
            int testAppointmentID)
        {
            int rowsAffected;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query =
                    @"UPDATE TestAppointments
                      SET IsLocked = 1
                      WHERE TestAppointmentID =
                            @TestAppointmentID";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@TestAppointmentID",
                        testAppointmentID);

                    try
                    {
                        connection.Open();
                        rowsAffected = command.ExecuteNonQuery();
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return rowsAffected > 0;
        }

        public static bool HasActiveAppointment(
            int localDrivingLicenseApplicationID,
            int testTypeID)
        {
            bool exists = false;

            using (SqlConnection connection =
                   new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query =
                    @"SELECT 1
                      FROM TestAppointments
                      WHERE LocalDrivingLicenseApplicationID =
                            @LocalDrivingLicenseApplicationID
                        AND TestTypeID = @TestTypeID
                        AND IsLocked = 0";

                using (SqlCommand command =
                       new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue(
                        "@LocalDrivingLicenseApplicationID",
                        localDrivingLicenseApplicationID);

                    command.Parameters.AddWithValue(
                        "@TestTypeID",
                        testTypeID);

                    try
                    {
                        connection.Open();
                        exists = command.ExecuteScalar() != null;
                    }
                    catch
                    {
                        exists = false;
                    }
                }
            }

            return exists;
        }
    }
}