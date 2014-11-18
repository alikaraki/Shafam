using System.Configuration;
using Microsoft.Practices.Unity;
using Shafam.Common.Infrastructure;
using Shafam.DataAccess.Infrastructure;
using Shafam.DataAccess.Repositories;

namespace Shafam.DataAccess
{
    public class ContainerConfigurator : ConfigurationSection, IContainerConfigurator
    {
        public void Configure(IUnityContainer container)
        {
            container.RegisterSingleton<IDatabaseInitializer, ShafamDropCreateAlwaysInitializer>();
            container.RegisterSingleton<IShafamDataContext, ShafamDataContext>();
            container.RegisterSingleton<IPatientRepository, PatientRepository>();
            container.RegisterSingleton<IDoctorRepository, DoctorRepository>();
            container.RegisterSingleton<IUserRepository, UserRepository>();
            container.RegisterSingleton<IMedicationRepository, MedicationRepository>();

            InitializeDatabase(container);
        }

        private static void InitializeDatabase(IUnityContainer container)
        {
            container.Resolve<ShafamDropCreateAlwaysInitializer>().Initialize();
        }
    }
}
