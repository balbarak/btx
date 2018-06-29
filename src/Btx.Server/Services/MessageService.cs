using Btx.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Services
{
    public class MessageService : ServiceBase<MessageService>
    {

        public Message Add(Message entity)
        {
            return repository.Create(entity);
        }

        public Message Update(Message entity)
        {
            return repository.Update(entity);
        }

        public Message GetById(string id)
        {
            return repository.Get<Message>(a => a.Id == id).FirstOrDefault();
        }
    }
}
