using System;
using System.Collections.Generic;
using System.Text;

namespace Btx.Client.Application.Services
{
    public class ServiceBase<TService> where TService : class, new()
    {
        protected static TService instance;

        public static TService Instance { get { return instance; } }

        static ServiceBase()
        {
            instance = new TService();
        }

        protected ServiceBase()
        {

        }

    }
}
