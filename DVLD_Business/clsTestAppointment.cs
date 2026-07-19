using System;
using System.Data;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class clsTestAppointment
    {
        public enum enMode
        {
            AddNew = 0,
            Update = 1
        }

        public enMode Mode { get; private set; }

        public int TestAppointmentID { get; set; }
        public int TestTypeID { get; set; }

        public int LocalDrivingLicenseApplicationID { get; set; }

        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsLocked { get; set; }
        public int RetakeTestApplicationID { get; set; }

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LocalDrivingLicenseApplicationID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            IsLocked = false;
            RetakeTestApplicationID = -1;

            Mode = enMode.AddNew;
        }

        private clsTestAppointment(
            int testAppointmentID,
            int testTypeID,
            int localDrivingLicenseApplicationID,
            DateTime appointmentDate,
            decimal paidFees,
            int createdByUserID,
            bool isLocked,
            int retakeTestApplicationID)
        {
            TestAppointmentID = testAppointmentID;
            TestTypeID = testTypeID;

            LocalDrivingLicenseApplicationID =
                localDrivingLicenseApplicationID;

            AppointmentDate = appointmentDate;
            PaidFees = paidFees;
            CreatedByUserID = createdByUserID;
            IsLocked = isLocked;

            RetakeTestApplicationID =
                retakeTestApplicationID;

            Mode = enMode.Update;
        }

        private bool _AddNewTestAppointment()
        {
            TestAppointmentID =
                clsTestAppointmentData.AddNewTestAppointment(
                    TestTypeID,
                    LocalDrivingLicenseApplicationID,
                    AppointmentDate,
                    PaidFees,
                    CreatedByUserID,
                    IsLocked,
                    RetakeTestApplicationID);

            return TestAppointmentID != -1;
        }

        private bool _UpdateTestAppointment()
        {
            return clsTestAppointmentData.UpdateTestAppointment(
                TestAppointmentID,
                TestTypeID,
                LocalDrivingLicenseApplicationID,
                AppointmentDate,
                PaidFees,
                CreatedByUserID,
                IsLocked,
                RetakeTestApplicationID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {
                        Mode = enMode.Update;
                        return true;
                    }

                    return false;

                case enMode.Update:
                    return _UpdateTestAppointment();

                default:
                    return false;
            }
        }

        public static clsTestAppointment Find(
            int testAppointmentID)
        {
            int testTypeID = -1;
            int localDrivingLicenseApplicationID = -1;
            DateTime appointmentDate = DateTime.Now;
            decimal paidFees = 0;
            int createdByUserID = -1;
            bool isLocked = false;
            int retakeTestApplicationID = -1;

            bool found =
                clsTestAppointmentData
                    .GetTestAppointmentInfoByID(
                        testAppointmentID,
                        ref testTypeID,
                        ref localDrivingLicenseApplicationID,
                        ref appointmentDate,
                        ref paidFees,
                        ref createdByUserID,
                        ref isLocked,
                        ref retakeTestApplicationID);

            if (!found)
                return null;

            return new clsTestAppointment(
                testAppointmentID,
                testTypeID,
                localDrivingLicenseApplicationID,
                appointmentDate,
                paidFees,
                createdByUserID,
                isLocked,
                retakeTestApplicationID);
        }

        public static DataTable
            GetTestAppointmentsByLocalApplicationID(
                int localDrivingLicenseApplicationID,
                int testTypeID)
        {
            return clsTestAppointmentData
                .GetTestAppointmentsByLocalApplicationID(
                    localDrivingLicenseApplicationID,
                    testTypeID);
        }

        public bool Lock()
        {
            if (TestAppointmentID == -1)
                return false;

            if (clsTestAppointmentData.LockTestAppointment(
                    TestAppointmentID))
            {
                IsLocked = true;
                return true;
            }

            return false;
        }

        public static bool HasActiveAppointment(
            int localDrivingLicenseApplicationID,
            int testTypeID)
        {
            return clsTestAppointmentData
                .HasActiveAppointment(
                    localDrivingLicenseApplicationID,
                    testTypeID);
        }
    }
}