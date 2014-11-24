using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<Notification> SendNotification(int SenderId, int ReceiverId, int PatientId, NotificationType notificationType)
        {
            List<Notification> notificationList = new List<Notification>();
            switch(notificationType)
            {
                case NotificationType.Referral:
                     
                    IEnumerable<Referral> referrals = _referralRepository.GetReferralsForDoctor(ReceiverId);
                    foreach (Referral referral in referrals)
                    { 
                        Patient referralPatient = _patientRepository.GetPatient(referral.PatientId);
                        Doctor referringDoctor = _doctorRepository.GetDoctor(referral.ReferringDoctorId);
                        Doctor referredDoctor = _doctorRepository.GetDoctor(referral.ReferredDoctorId);
                        string message = "Dear Dr." + referredDoctor.LastName + ", patient " + referralPatient.FirstName + " " +
                                            referralPatient.LastName + " has been referred to you by Dr. " + referringDoctor.LastName + ".";
                        Notification notification = new Notification
                        {
                            Message = message,
                            SenderId = referringDoctor.DoctorId,
                            ReceiverId = referredDoctor.DoctorId
                        };
                        notificationList.Add(notification);
                    }
                    break;
        

                case NotificationType.TestResult:
                    Doctor doctor = _doctorRepository.GetDoctor(ReceiverId);
                    Staff staff = _staffRepository.GetStaff(SenderId);
                    Patient testPatient = _patientRepository.GetPatient(PatientId);
                    string testMessage = "Dear Dr. " + doctor.LastName + ", test results for patient " + testPatient.FirstName +
                                        " " + testPatient.LastName + " have been uploaded.";
                    Notification testNotification = new Notification 
                    {
                            Message = testMessage,
                            SenderId =staff.StaffId,
                            ReceiverId = doctor.DoctorId
                    };
                    notificationList.Add(testNotification);
                    break;
                                                            
                case NotificationType.AppointmentRequest:
                    List<AppointmentRequest> appRequests = _appointmentRequestRepository.GetAppointmentRequestsForPatient(SenderId);
                    foreach (AppointmentRequest appRequest in appRequests)
                    {
                        Patient patient = _patientRepository.GetPatient(SenderId);
                        Staff appRequestStaff = _staffRepository.GetStaff(ReceiverId);
                        string message = "Dear staff member, patient " + patient.FirstName + " " + patient.LastName +
                                            " has requested an appointment.";
                        Notification appRequestNotification = new Notification
                        {
                            Message = message,
                            SenderId = patient.PatientId,
                            ReceiverId = appRequestStaff.StaffId
                        };
                        notificationList.Add(appRequestNotification);
                    }
                    break;
            }
            return (notificationList);
                                
        }

        public void ViewNotification()
        {
            throw new NotImplementedException();
        }

        public void MarkAsSeen(int referralId)
        {
            throw new NotImplementedException();
        }

    }
}
