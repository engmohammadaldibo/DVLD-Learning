using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsUser
    {
        private enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        private enMode Mode;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public clsPerson PersonInfo { get; set; }

        public clsUser()
        {
            UserID = -1;
            PersonID = -1;
            UserName = "";
            Password = "";
            IsActive = true;
            PersonInfo = null;

            Mode = enMode.AddNew;
        }

        private clsUser(
            int UserID,
            int PersonID,
            string UserName,
            string Password,
            bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            PersonInfo = clsPerson.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            UserID = clsUserData.AddNewUser(
                PersonID,
                UserName,
                Password,
                IsActive);

            return UserID != -1;
        }

        private bool _UpdateUser()
        {
            return clsUserData.UpdateUser(
                UserID,
                PersonID,
                UserName,
                Password,
                IsActive);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:

                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:

                    return _UpdateUser();
            }

            return false;
        }

        public static clsUser Find(int UserID)
        {
            int PersonID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            bool isFound =
                clsUserData.GetUserInfoByUserID(
                    UserID,
                    ref PersonID,
                    ref UserName,
                    ref Password,
                    ref IsActive);

            if (isFound)
            {
                return new clsUser(
                    UserID,
                    PersonID,
                    UserName,
                    Password,
                    IsActive);
            }

            return null;
        }

        public static clsUser FindByPersonID(int PersonID)
        {
            int UserID = -1;
            string UserName = "";
            string Password = "";
            bool IsActive = false;

            bool isFound =
                clsUserData.GetUserInfoByPersonID(
                    PersonID,
                    ref UserID,
                    ref UserName,
                    ref Password,
                    ref IsActive);

            if (isFound)
            {
                return new clsUser(
                    UserID,
                    PersonID,
                    UserName,
                    Password,
                    IsActive);
            }

            return null;
        }

        public static clsUser Find(
            string UserName,
            string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;

            bool isFound =
                clsUserData.GetUserInfoByUserNameAndPassword(
                    UserName,
                    Password,
                    ref UserID,
                    ref PersonID,
                    ref IsActive);

            if (isFound)
            {
                return new clsUser(
                    UserID,
                    PersonID,
                    UserName,
                    Password,
                    IsActive);
            }

            return null;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
    }
}