using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Shafam.Common.DataModel;
using Shafam.Common.Infrastructure;
using Shafam.DataAccess;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer _container;

        private IDoctorRepository _doctorRepository;
        private IPatientRepository _patientRepository;
        private IAccountRepository _accountRepository;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeDependencyInjectionContainer();

            AddTestData();
        }

        private static void InitializeDependencyInjectionContainer()
        {
            _container = new UnityContainer();
            LoadConfigurations(_container);

            DependencyResolver.SetResolver(new UnityDependencyResolver(_container));
        }

        private static void LoadConfigurations(IUnityContainer container)
        {
            var dataAccessConfiguration = ConfigurationManager.GetSection("dataAccessConfiguration") as IContainerConfigurator;
            var businessLogicConfiguration = ConfigurationManager.GetSection("businessLogicConfiguration") as IContainerConfigurator;

            ConfigureModule(dataAccessConfiguration, container);
            ConfigureModule(businessLogicConfiguration, container);
        }

        private static void ConfigureModule(IContainerConfigurator configurator, IUnityContainer container)
        {
            if (configurator != null)
            {
                configurator.Configure(container);
            }
        }

        private void ResolveRepositories()
        {
            _doctorRepository = _container.Resolve<IDoctorRepository>();
            _patientRepository = _container.Resolve<IPatientRepository>();
            _accountRepository = _container.Resolve<IAccountRepository>();
        }

        private void AddTestData()
        {
            ResolveRepositories();

            var doctor1 = new Doctor
            {
                FirstName = "John",
                LastName = "Smith",
                Gender = Gender.Male,
                Specialty = "Family doctor"
            };

            var doctor2 = new Doctor
            {
                FirstName = "Amy",
                LastName = "Montrose",
                Gender = Gender.Male,
                Specialty = "Neurologist"
            };

            _doctorRepository.AddDoctor(doctor1);
            _doctorRepository.AddDoctor(doctor2);

            var doctor1Account = new Account { Username = "John", Password = "john", Role = UserRole.Doctor, UserId = doctor1.DoctorId };
            var doctor2Account = new Account { Username = "Amy", Password = "amy", Role = UserRole.Doctor, UserId = doctor2.DoctorId };

            _accountRepository.CreateAccount(doctor1Account);
            _accountRepository.CreateAccount(doctor2Account);

            var patients = new List<Patient>()
                                  {
                                      new Patient {FirstName = "Harold", LastName = "Truett", Gender = Gender.Male},
                                      new Patient {FirstName = "Elinor", LastName = "Rosenow", Gender = Gender.Female},
                                      new Patient {FirstName = "Meta", LastName = "Tutor", Gender = Gender.Female},
                                      new Patient {FirstName = "Derick", LastName = "Ranallo", Gender = Gender.Male},
                                      new Patient {FirstName = "Flossie", LastName = "Brase", Gender = Gender.Female},
                                      new Patient {FirstName = "Kaylene", LastName = "Pease", Gender = Gender.Female},
                                      new Patient {FirstName = "Jarrod", LastName = "Kenley", Gender = Gender.Male},
                                  };

            patients.ForEach(p => _patientRepository.AddPatient(p));

            _doctorRepository.AssignPatient(doctor1.DoctorId, patients[0].PatientId);
            _doctorRepository.AssignPatient(doctor1.DoctorId, patients[2].PatientId);
            _doctorRepository.AssignPatient(doctor1.DoctorId, patients[3].PatientId);
            _doctorRepository.AssignPatient(doctor1.DoctorId, patients[6].PatientId);

            _doctorRepository.AssignPatient(doctor2.DoctorId, patients[1].PatientId);
            _doctorRepository.AssignPatient(doctor2.DoctorId, patients[2].PatientId);
            _doctorRepository.AssignPatient(doctor2.DoctorId, patients[3].PatientId);
            _doctorRepository.AssignPatient(doctor2.DoctorId, patients[4].PatientId);
            _doctorRepository.AssignPatient(doctor2.DoctorId, patients[5].PatientId);
        }
    }
}