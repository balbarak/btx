using Btx.Server.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Services
{
    public class ServiceBase<TService> where TService : class, new()
    {
        protected static TService _instance;

        protected GenericRepository _repository;

        protected string[] Includes { get; set; }

        public static TService Instance { get { return _instance; } }

        protected ServiceBase()
        {
            this._repository = new GenericRepository();
        }

        static ServiceBase()
        {
            _instance = new TService();
        }

    }

}
