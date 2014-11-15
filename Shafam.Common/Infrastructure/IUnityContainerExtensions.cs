using Microsoft.Practices.Unity;

namespace Shafam.Common.Infrastructure
{
    public static class IUnityContainerExtensions
    {
        public static IUnityContainer RegisterSingleton<IT, T>(this IUnityContainer container)
               where T : IT
        {
            return container.RegisterType<IT, T>(new ContainerControlledLifetimeManager());
        }
    }
}