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
            container.RegisterSingleton<IAccountRepository, AccountRepository>();
            container.RegisterSingleton<IMedicationRepository, MedicationRepository>();
            container.RegisterSingleton<IVisitationRepository, VisitationRepository>();
            container.RegisterSingleton<IAppointmentRepository, AppointmentRepository>();
            container.RegisterSingleton<IAppointmentRequestRepository, AppointmentRequestRepository>();
            container.RegisterSingleton<IStaffRepository, StaffRepository>();
            container.RegisterSingleton<ITreatmentRepository, TreatmentRepository>();
            container.RegisterSingleton<ITestRepository, TestRepository>();
            container.RegisterSingleton<IReferralRepository, ReferralRepository>();
            InitializeDatabase(container);
        }

        private static void InitializeDatabase(IUnityContainer container)
        {
            container.Resolve<ShafamMigrateDatabaseInitializer>().Initialize();
        }
    }
}
