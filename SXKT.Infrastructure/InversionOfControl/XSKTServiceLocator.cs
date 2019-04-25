using Microsoft.Extensions.DependencyInjection;
using System;

namespace SXKT.Infrastructure.InversionOfControl
{
    public class SXKTServiceLocator
    {
        private readonly ServiceProvider _currentServiceProvider;
        private static ServiceProvider _serviceProvider;

        public SXKTServiceLocator(ServiceProvider currentServiceProvider)
        {
            _currentServiceProvider = currentServiceProvider;
        }

        public static SXKTServiceLocator Current => new SXKTServiceLocator(_serviceProvider);

        public static void SetLocatorProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetInstance(Type serviceType)
        {
            return _currentServiceProvider.GetService(serviceType);
        }

        public TService GetInstance<TService>()
        {
            return _currentServiceProvider.GetService<TService>();
        }
    }
}