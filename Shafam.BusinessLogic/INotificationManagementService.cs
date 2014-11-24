using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface INotificationManagementService
    {
        bool SendNotification(int referringDoctorId, int referredDoctorId);

        void ViewNotification();

        void MarkAsSeen(int notificationId);
    }
}
