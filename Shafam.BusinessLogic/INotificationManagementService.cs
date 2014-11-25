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

        //List<Notification> SendNotification(int senderId, int receiverId, int patientId, NotificationType notificationType);

        void MarkAsSeen(int notificationId, NotificationType type);

        IEnumerable<Notification> GetNotificationsForDoctor(int doctorId);

        IEnumerable<Notification> GetNotificationsForStaff(int staffId);
    }
}
