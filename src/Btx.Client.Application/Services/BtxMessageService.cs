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
            using (UnitOfWork work = new UnitOfWork())
            {
                var users = await work.GenericRepository.GetAsync<BtxUser>(a => a.Id == entity.RecipientId);
                var user = users.FirstOrDefault();

                if (user == null)
                {
                    entity.Recipient = new BtxUser()
                    {
                        Id = entity.RecipientId,
                        Username = entity.Recipient?.Username
                    };

                    entity.RecipientId = null;
                }
                else
                {
                    entity.RecipientId = user.Id;
                    entity.Recipient = null;
                }

                entity = await work.GenericRepository.CreateAsync(entity);

                await work.CommitAsync();
            }
            
            return entity;
        }

        public async Task<List<BtxMessage>> GetByThreadId(string threadId)
        {
            return await _repository.GetAsync<BtxMessage>(a => a.ThreadId == threadId,orderBy:p=> p.OrderBy(f=> f.Date)).ConfigureAwait(false);
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
