using System.Configuration;
using Microsoft.Practices.Unity;
using Shafam.BusinessLogic.Authentication;
using Shafam.BusinessLogic.PatientManagement;
using Shafam.Common.Infrastructure;
<<<<<<< HEAD
using Shafam.BusinessLogic.NotificationManagement;
=======
using Shafam.BusinessLogic.Notification;
using Shafam.BusinessLogic.Scheduling;
>>>>>>> FETCH_HEAD

namespace Shafam.BusinessLogic
{
    public class ContainerConfigurator : ConfigurationSection, IContainerConfigurator
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterSingleton<IIdentityProvider, IdentityProvider>();
            container.RegisterSingleton<IPatientManagementService, PatientManagementService>();
            container.RegisterSingleton<INotificationManagementService, NotificationManagementService>();
            container.RegisterSingleton<ISchedulingService, SchedulingService>();
        }
    }
}