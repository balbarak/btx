using Btx.Server.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Server.Services
{
    public class MessageService : ServiceBase<MessageService>
    {
        public MessageService()
        {
            Includes = new[]
            {
                nameof(Message.FromUser),
                nameof(Message.ToUser)
            };
        }

        public Message Add(Message entity)
        {
            return _repository.Create(entity);
        }

        public Message Update(Message entity)
        {
            return _repository.Update(entity);
        }

        public Message GetById(string id)
        {
            return _repository.Get<Message>(a => a.Id == id).FirstOrDefault();
        }

        public List<Message> GetPendingMessages(string toUserId)
        {
            return _repository.Get<Message>(
                a => a.ToUserId == toUserId && 
                a.Status == MessageStatus.ServerDelivered,
                includeProperties:Includes)
                .ToList();
        }
    }
}
