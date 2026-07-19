using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsApplication
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enum enApplicationStatus
        {
            New = 1,
            Cancelled = 2,
            Completed = 3
        }

        public enMode Mode { get; set; }

        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }

        public clsApplication()
        {
            ApplicationID = -1;
            ApplicantPersonID = -1;
            ApplicationDate = DateTime.Now;
            ApplicationTypeID = -1;
            ApplicationStatus = enApplicationStatus.New;
            LastStatusDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        private clsApplication(
            int applicationID,
            int applicantPersonID,
            DateTime applicationDate,
            int applicationTypeID,
            enApplicationStatus applicationStatus,
            DateTime lastStatusDate,
            float paidFees,
            int createdByUserID)
        {
            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;

            Mode = enMode.Update;
        }

        private bool _AddNewApplication()
        {
            ApplicationID = clsApplicationData.AddNewApplication(
                ApplicantPersonID,
                ApplicationDate,
                ApplicationTypeID,
                (byte)ApplicationStatus,
                LastStatusDate,
                PaidFees,
                CreatedByUserID);

            return ApplicationID != -1;
        }

        private bool _UpdateApplication()
        {
            return clsApplicationData.UpdateApplication(
                ApplicationID,
                ApplicantPersonID,
                ApplicationDate,
                ApplicationTypeID,
                (byte)ApplicationStatus,
                LastStatusDate,
                PaidFees,
                CreatedByUserID);
        }

        public static clsApplication Find(int applicationID)
        {
            int applicantPersonID = -1;
            DateTime applicationDate = DateTime.Now;
            int applicationTypeID = -1;
            byte applicationStatus = 1;
            DateTime lastStatusDate = DateTime.Now;
            float paidFees = 0;
            int createdByUserID = -1;

            bool found = clsApplicationData.GetApplicationInfoByID(
                applicationID,
                ref applicantPersonID,
                ref applicationDate,
                ref applicationTypeID,
                ref applicationStatus,
                ref lastStatusDate,
                ref paidFees,
                ref createdByUserID);

            if (!found)
                return null;

            return new clsApplication(
                applicationID,
                applicantPersonID,
                applicationDate,
                applicationTypeID,
                (enApplicationStatus)applicationStatus,
                lastStatusDate,
                paidFees,
                createdByUserID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:
                    return _UpdateApplication();

                default:
                    return false;
            }
        }

        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }

        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 2);
        }

        public bool Complete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, 3);
        }

        public static DataTable GetAllApplications()
        {
            return clsApplicationData.GetAllApplications();
        }

        public static bool IsApplicationExist(int applicationID)
        {
            return clsApplicationData.IsApplicationExist(applicationID);
        }

        public static bool DoesPersonHaveActiveApplication(
            int personID,
            int applicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(
                personID,
                applicationTypeID);
        }

        public static int GetActiveApplicationID(
            int personID,
            int applicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(
                personID,
                applicationTypeID);
        }
    }
}