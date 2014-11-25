﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shafam.Common.DataModel;

namespace Shafam.BusinessLogic
{
    public interface INotificationManagementService
    {
<<<<<<< HEAD
        List<Notification> SendNotification(int senderId, int receiverId, int patientId, NotificationType notificationType);
=======
        void MarkAsSeen(int notificationId, NotificationType type);
>>>>>>> a1899c515a8a1fa82b95b8a4f2de47398ba6e159

        IEnumerable<Notification> GetNotificationsForDoctor(int doctorId);

        IEnumerable<Notification> GetNotificationsForStaff(int staffId);
    }
}
