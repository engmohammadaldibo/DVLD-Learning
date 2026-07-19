using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicenseClassData
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM LicenseClasses
                             ORDER BY ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}