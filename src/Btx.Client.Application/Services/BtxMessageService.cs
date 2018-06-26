using Btx.Client.Application.Persistance;
using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Btx.Client.Application.Services
{
    public class BtxMessageService : ServiceBase<BtxMessageService>
    {
        public async Task<BtxMessage> Add(BtxMessage entity)
        {
            entity = await _repository.CreateAsync(entity);

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

        public BtxMessage GetLastMessageByThreadId(string threadId)
        {
            BtxMessage result;

            using (BtxDbContext context = new BtxDbContext())
            {
                result = context.BtxMessages.Where(a => a.ThreadId == threadId).OrderByDescending(a => a.Date).FirstOrDefault();
            }

            return result;
        }
    }
}
