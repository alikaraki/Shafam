using System.Configuration;
using Microsoft.Practices.Unity;
using Shafam.BusinessLogic.PatientManagement;
using Shafam.Common.Infrastructure;
using Shafam.BusinessLogic.Notification;

namespace Shafam.BusinessLogic
{
    public class ContainerConfigurator : ConfigurationSection, IContainerConfigurator
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterSingleton<IPatientManagementService, PatientManagementService>();
            container.RegisterSingleton<INotificationManagementService, NotificationManagementService>();
        }
    }
}