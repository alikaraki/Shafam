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
        void SendNotification(int referringDocId, int referredDocId);

        void ViewNotification();
    }
}
