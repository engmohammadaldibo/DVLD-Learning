using DVLD_Business;

namespace DVLD
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser { get; set; }

        public static bool IsUserLoggedIn
        {
            get
            {
                return CurrentUser != null &&
                       CurrentUser.UserID != -1;
            }
        }

        public static void ClearCurrentUser()
        {
            CurrentUser = null;
        }
    }
}