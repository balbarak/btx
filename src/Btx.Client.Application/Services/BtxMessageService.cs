using Btx.Client.Application.Persistance;
using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Btx.Client.Application.Services
{
    public class BtxMessageService : ServiceBase<BtxMessageService>
    {
        public BtxMessage Add(BtxMessage entity)
        {
            using (BtxDbContext context = new BtxDbContext())
            {
                context.BtxMessages.Add(entity);
                context.SaveChanges();
            }

            return entity;
        }

        public List<BtxMessage> GetByThreadId(string threadId)
        {
            List<BtxMessage> result = new List<BtxMessage>();

            using (BtxDbContext context = new BtxDbContext())
            {
                result = context.BtxMessages.Where(a => a.ThreadId == threadId).ToList();
            }

            return result;
        }
    }
}
