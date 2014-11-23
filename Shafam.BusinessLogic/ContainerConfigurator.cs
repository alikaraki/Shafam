using System.Configuration;
using Microsoft.Practices.Unity;
using Shafam.BusinessLogic.AccountManagement;
using Shafam.BusinessLogic.Authentication;
using Shafam.BusinessLogic.PatientManagement;
using Shafam.Common.Infrastructure;
using Shafam.BusinessLogic.NotificationManagement;
using Shafam.BusinessLogic.Scheduling;
using Shafam.BusinessLogic.VisitationManagement;


namespace Shafam.BusinessLogic
{
    public class ContainerConfigurator : ConfigurationSection, IContainerConfigurator
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterSingleton<IIdentityProvider, IdentityProvider>();
            container.RegisterSingleton<IAccountManagementService, AccountManagementService>();
            container.RegisterSingleton<IPatientManagementService, PatientManagementService>();
            container.RegisterSingleton<INotificationManagementService, NotificationManagementService>();
            container.RegisterSingleton<ISchedulingService, SchedulingService>();
            container.RegisterSingleton<IVisitationManagementService, VisitationManagementService>();

        }
    }
}