using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsApplicationTypeData
    {
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection =
                new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT * FROM ApplicationTypes
                             ORDER BY ApplicationTypeTitle";

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