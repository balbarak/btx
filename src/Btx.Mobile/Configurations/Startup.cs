using Btx.Client.Application.Persistance;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Mobile
{
    public class Startup
    {
        static private readonly ServiceCollection _serviceCollection = new ServiceCollection();

        static public void Configure()
        {
            ServiceLocator.Configure(_serviceCollection);
            
        }
    }
}
