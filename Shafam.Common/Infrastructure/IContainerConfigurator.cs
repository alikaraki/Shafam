using Microsoft.Practices.Unity;

namespace Shafam.Common.Infrastructure
{
    public interface IContainerConfigurator
    {
        /// <summary>
        /// Register types with the container.
        /// </summary>
        void Configure(IUnityContainer container);
    }
}