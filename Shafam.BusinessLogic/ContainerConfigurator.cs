using System.Configuration;
using Microsoft.Practices.Unity;
using Shafam.Common.Infrastructure;

namespace Shafam.BusinessLogic
{
    public class ContainerConfigurator : ConfigurationSection, IContainerConfigurator
    {
        public void Configure(IUnityContainer container)
        {
        }
    }
}
