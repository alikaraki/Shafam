using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Shafam.Common.Infrastructure;
using Shafam.UserInterface.Infrastructure;

namespace Shafam.UserInterface
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IUnityContainer _container;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            InitializeDependencyInjectionContainer();
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
    }
}