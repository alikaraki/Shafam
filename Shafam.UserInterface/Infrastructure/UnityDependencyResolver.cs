using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Shafam.UserInterface.Infrastructure
{
    /// <summary>
    /// Dependency resolver implementation that uses Unity.
    /// </summary>
    public class UnityDependencyResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }
    }
}