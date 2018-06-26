using Btx.Mobile.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile
{
    public class ServiceLocator : IDisposable
    {
        static private readonly ConcurrentDictionary<int, ServiceLocator> _serviceLocators = new ConcurrentDictionary<int, ServiceLocator>();

        static private ServiceProvider _rootServiceProvider = null;

        private IServiceScope _serviceScope = null;

        static public ServiceLocator Current
        {
            get
            {
                return _serviceLocators.GetOrAdd(1, key => new ServiceLocator());
            }
        }
        
        public ServiceLocator()
        {
            _serviceScope = _rootServiceProvider.CreateScope();
        }

        static public void Configure(IServiceCollection serviceCollections)
        {
            //To add dependency injection

            //serviceCollections.AddScoped<IDataService>(opt =>
            //{
            //    return new DataService(AppConfig.SQLITE_FILE_PATH);
            //});


            serviceCollections.AddScoped<ChatListViewModel>();

            _rootServiceProvider = serviceCollections.BuildServiceProvider();

        }

        public T GetService<T>()
        {
            return GetService<T>(true);
        }

        public T GetService<T>(bool isRequired)
        {
            if (isRequired)
            {
                return _serviceScope.ServiceProvider.GetRequiredService<T>();
            }
            return _serviceScope.ServiceProvider.GetService<T>();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_serviceScope != null)
                {
                    _serviceScope.Dispose();
                }
            }
        }

    }
}
