using Btx.Server.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Services
{
    public class ServiceBase<TService> where TService : class, new()
    {
        protected static TService instance;

        protected GenericRepository repository;

        protected string[] Includes { get; set; }

        public static TService Instance { get { return instance; } }

        protected ServiceBase()
        {
            this.repository = new GenericRepository();
        }

        static ServiceBase()
        {
            instance = new TService();
        }

    }

}
