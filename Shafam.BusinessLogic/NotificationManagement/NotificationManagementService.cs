using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using Shafam.Common.DataModel;
using Shafam.DataAccess;

namespace Shafam.BusinessLogic.NotificationManagement
{
    public class NotificationManagementService : INotificationManagementService
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly ITestRepository _testRepository;
        private readonly IAppointmentRequestRepository _appointmentRequestRepository;
        private readonly IReferralRepository _referralRepository;
        private readonly IStaffRepository _staffRepository;

        public NotificationManagementService (IDoctorRepository doctorRepository,
                                              IPatientRepository patientRepository, 
                                              ITestRepository testRepository,
                                              IAppointmentRequestRepository appointmentRequestRepository,
                                              IReferralRepository referralRepository,
                                                IStaffRepository staffRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _testRepository = testRepository;
            _appointmentRequestRepository = appointmentRequestRepository;
            _referralRepository = referralRepository;
            _staffRepository = staffRepository;
        }


        public void MarkAsSeen(int notificationId, NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Referral:
                    _referralRepository.MarkAsSeen(notificationId);
                    break;

                case NotificationType.TestResult:
                    _testRepository.MarkAsSeen(notificationId);
                    break;

                case NotificationType.AppointmentRequest:
                    _appointmentRequestRepository.MarkAsSeen(notificationId);
                    break;
            }
        }

        public IEnumerable<Notification> GetNotificationsForDoctor(int doctorId)
        {
            var notificationList = new List<Notification>();

            notificationList.AddRange(GetReferralNotifications(doctorId));
            notificationList.AddRange(GetTestResultNotifications(doctorId));

            return notificationList;
        }

        public IEnumerable<Notification> GetNotificationsForStaff(int staffId)
        {
            var notifications = new List<Notification>();

            Staff staff = _staffRepository.GetStaff(staffId);

            if (!staff.Department.HasValue)
            {
                return notifications;
            }

            IEnumerable<Doctor> doctors = _doctorRepository.GetDoctorsInDepartment(staff.Department.Value);
            foreach (var doctor in doctors)
            {
                IEnumerable<Patient> patients = _doctorRepository.GetPatientsForDoctor(doctor.DoctorId);
                var requests = new List<AppointmentRequest>();
                patients.ForEach(p => requests.AddRange(_appointmentRequestRepository.GetAppointmentRequestsForPatient(p.PatientId).Where(r => !r.SeenByStaff)));

                notifications.AddRange(requests.Select(
                    r => new Notification
                         {
                             NotificationId = r.AppointmentRequestId,
                             PatientId = r.PatientId,
                             Type = NotificationType.AppointmentRequest,
                             Message = "Patient " + GetPatientName(r.PatientId) + " requested an appointment " + (string.IsNullOrEmpty(r.Reason) ? "" : " - " + r.Reason)  
                         }));
            }

            return notifications;
        }

        private string GetPatientName(int patientId)
        {
            Patient patient = _patientRepository.GetPatient(patientId);
            return patient.FirstName + " " + patient.LastName + " (" + patientId + ")";
        }

        private IEnumerable<Notification> GetReferralNotifications(int doctorId)
        {
            var notifications = new List<Notification>();

            IEnumerable<Referral> referrals = _referralRepository.GetReferralsForReferredDoctor(doctorId);
            foreach (Referral referral in referrals)
            {
                if (referral.SeenByReferredDoctor)
                {
                    continue;
                }

                Patient referralPatient = _patientRepository.GetPatient(referral.PatientId);
                Doctor referringDoctor = _doctorRepository.GetDoctor(referral.ReferringDoctorId);

                string message = "Patient " + referralPatient.FirstName + " " +
                                 referralPatient.LastName + " has been referred to you by Dr. " +
                                 referringDoctor.LastName + ".";

                notifications.Add(new Notification
                                  {
                                      NotificationId = referral.ReferralId, 
                                      Type = NotificationType.Referral, 
                                      Message = message,
                                      PatientId = referral.PatientId
                                  });
            }

            return notifications;
        }

        private IEnumerable<Notification> GetTestResultNotifications(int doctorId)
        {
            var notifications = new List<Notification>();

            IEnumerable<Patient> patients = _doctorRepository.GetPatientsForDoctor(doctorId);
            List<Test> tests = new List<Test>();
            patients.ForEach(p => tests.AddRange(_testRepository.GetTestsForPatient(p.PatientId)));

            foreach (var test in tests)
            {
                if (test.Completed == null || test.SeenByDoctor)
                {
                    continue;
                }

                Patient patient = _patientRepository.GetPatient(test.PatientId);
                string message = "Test results for patient " + patient.FirstName +
                                       " " + patient.LastName + " are now available.";

                notifications.Add(new Notification
                                  {
                                      NotificationId = test.TestId, 
                                      Message = message, 
                                      Type = NotificationType.TestResult,
                                      PatientId = test.PatientId,
                                  });
            }

            return notifications;
        }

        //public bool SendNotification(int referringDoctorId, int referredDoctorId)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
