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

       

        public NotificationManagementService (IDoctorRepository doctorRepository,
                                              IPatientRepository patientRepository, 
                                              ITestRepository testRepository,
                                              IAppointmentRequestRepository appointmentRequestRepository)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _testRepository = testRepository;
            _appointmentRequestRepository = appointmentRequestRepository;
        }

        public bool SendNotification(int SenderId, int ReceiverId, NotificationType notificationType)
        {
            switch()

            
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
