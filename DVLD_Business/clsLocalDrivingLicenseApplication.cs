using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enLocalMode
        {
            AddNew = 0,
            Update = 1
        }

        public enLocalMode LocalMode { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID = -1;
            LicenseClassID = -1;
            LocalMode = enLocalMode.AddNew;
        }

        private clsLocalDrivingLicenseApplication(
            int localDrivingLicenseApplicationID,
            int applicationID,
            int applicantPersonID,
            DateTime applicationDate,
            int applicationTypeID,
            enApplicationStatus applicationStatus,
            DateTime lastStatusDate,
            float paidFees,
            int createdByUserID,
            int licenseClassID)
        {
            LocalDrivingLicenseApplicationID =
                localDrivingLicenseApplicationID;

            ApplicationID = applicationID;
            ApplicantPersonID = applicantPersonID;
            ApplicationDate = applicationDate;
            ApplicationTypeID = applicationTypeID;
            ApplicationStatus = applicationStatus;
            LastStatusDate = lastStatusDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            LicenseClassID = licenseClassID;

            LocalMode = enLocalMode.Update;
            base.Mode = clsApplication.enMode.Update;
        }

        private bool _AddNewLocalDrivingLicenseApplication()
        {
            LocalDrivingLicenseApplicationID =
                clsLocalDrivingLicenseApplicationData
                    .AddNewLocalDrivingLicenseApplication(
                        ApplicationID,
                        LicenseClassID);

            return LocalDrivingLicenseApplicationID != -1;
        }

        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData
                .UpdateLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID,
                    ApplicationID,
                    LicenseClassID);
        }

        public static clsLocalDrivingLicenseApplication
            FindByLocalDrivingAppLicenseID(
                int localDrivingLicenseApplicationID)
        {
            int applicationID = -1;
            int licenseClassID = -1;

            bool found =
                clsLocalDrivingLicenseApplicationData
                    .GetLocalDrivingLicenseApplicationInfoByID(
                        localDrivingLicenseApplicationID,
                        ref applicationID,
                        ref licenseClassID);

            if (!found)
                return null;

            clsApplication application = clsApplication.Find(applicationID);

            if (application == null)
                return null;

            return new clsLocalDrivingLicenseApplication(
                localDrivingLicenseApplicationID,
                application.ApplicationID,
                application.ApplicantPersonID,
                application.ApplicationDate,
                application.ApplicationTypeID,
                application.ApplicationStatus,
                application.LastStatusDate,
                application.PaidFees,
                application.CreatedByUserID,
                licenseClassID);
        }

        public static clsLocalDrivingLicenseApplication
            FindByApplicationID(int applicationID)
        {
            int localDrivingLicenseApplicationID = -1;
            int licenseClassID = -1;

            bool found =
                clsLocalDrivingLicenseApplicationData
                    .GetLocalDrivingLicenseApplicationInfoByApplicationID(
                        applicationID,
                        ref localDrivingLicenseApplicationID,
                        ref licenseClassID);

            if (!found)
                return null;

            clsApplication application = clsApplication.Find(applicationID);

            if (application == null)
                return null;

            return new clsLocalDrivingLicenseApplication(
                localDrivingLicenseApplicationID,
                application.ApplicationID,
                application.ApplicantPersonID,
                application.ApplicationDate,
                application.ApplicationTypeID,
                application.ApplicationStatus,
                application.LastStatusDate,
                application.PaidFees,
                application.CreatedByUserID,
                licenseClassID);
        }

        public new bool Save()
        {
            base.Mode =
                LocalMode == enLocalMode.AddNew
                    ? clsApplication.enMode.AddNew
                    : clsApplication.enMode.Update;

            if (!base.Save())
                return false;

            switch (LocalMode)
            {
                case enLocalMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        LocalMode = enLocalMode.Update;
                        return true;
                    }

                    return false;

                case enLocalMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();

                default:
                    return false;
            }
        }

        public static DataTable
            GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData
                .GetAllLocalDrivingLicenseApplications();
        }

        public new bool Delete()
        {
            bool deleted =
                clsLocalDrivingLicenseApplicationData
                    .DeleteLocalDrivingLicenseApplication(
                        LocalDrivingLicenseApplicationID);

            if (!deleted)
                return false;

            return base.Delete();
        }
    }
}