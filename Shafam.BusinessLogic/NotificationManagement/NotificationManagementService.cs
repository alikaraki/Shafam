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
        IDoctorRepository _doctorRepository;
        ITestRepository _testRepository;

        public NotificationManagementService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public NotificationManagementService (IDoctorRepository doctorRepository, ITestRepository testRepository)
        {
            _doctorRepository = doctorRepository;
            _testRepository = testRepository;
        }

        public bool SendNotification(int referringDocId, int referredDocId)
        {
            return true; //if notification sent successfully
            throw new NotImplementedException();
        }

        public void ViewNotification()
        {
            throw new NotImplementedException();
        }
    }
}
