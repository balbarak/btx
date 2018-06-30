using Btx.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Services
{
    public class ConnectionService : ServiceBase<ConnectionService>
    {
        public Connection Add(Connection entity)
        {
            return _repository.Create(entity);
        }

        public Connection Update(Connection entity)
        {
            return _repository.Update(entity);
        }

        public Connection GetById(string id)
        {
            return _repository.Get<Connection>(a => a.Id == id).FirstOrDefault();
        }

        public List<Connection> GetActiveConnections(string userId)
        {
            return _repository.Get<Connection>(a => a.UserId == userId && a.IsConnected == true).ToList();
        }
    }
}
