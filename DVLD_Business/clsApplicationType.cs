using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplicationType
    {
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
    }
}