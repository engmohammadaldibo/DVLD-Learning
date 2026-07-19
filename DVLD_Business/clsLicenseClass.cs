using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLicenseClass
    {
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }
    }
}