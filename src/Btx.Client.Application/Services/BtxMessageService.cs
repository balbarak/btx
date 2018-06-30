using Btx.Client.Application.Persistance;
using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Btx.Client.Domain.Search;

namespace Btx.Client.Application.Services
{
    public class BtxMessageService : ServiceBase<BtxMessageService>
    {
        public BtxMessageService()
        {
            Includes = new[]
            {
                nameof(BtxMessage.Recipient),
                nameof(BtxMessage.Thread),

            };
        }

        public BtxMessage AddOrUpdate(BtxMessage entity)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                SetBtxUser(entity, work);

                var found = work.GenericRepository.Get<BtxMessage>(a => a.Id == entity.Id).FirstOrDefault();

                if (found == null)
                {
                    entity = work.GenericRepository.Create(entity);
                }
                else
                {
                    found = found.Update(entity);
                }

                work.Commit();
            }

            return entity;
        }

        public async Task<BtxMessage> AddAsync(BtxMessage entity)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                await SetBtxUserAsync(entity, work);

                entity = await work.GenericRepository.CreateAsync(entity).ConfigureAwait(false);

                await work.CommitAsync().ConfigureAwait(false);
            }

            return entity;
        }

        public BtxMessage Update(BtxMessage entity)
        {
            return _repository.Update(entity);
            
        }
        
        public async Task<SearchResult<BtxMessage>> Search(SearchCriteria<BtxMessage> search)
        {
            return await _repository.SearchAsync(search,Includes);
        }

        public async Task<BtxMessage> GetByIdAsync(string id)
        {
            var result = await _repository.GetAsync<BtxMessage>(a => a.Id == id, includeProperties: Includes);

            return result.FirstOrDefault();
        }

        public BtxMessage GetById(string id)
        {
            return _repository.Get<BtxMessage>(a => a.Id == id, includeProperties: Includes).FirstOrDefault();
            
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
        
        private async Task SetBtxUserAsync(BtxMessage entity, UnitOfWork work)
        {
            if (string.IsNullOrWhiteSpace(entity.RecipientId))
                return;

            var users = await work.GenericRepository.GetAsync<BtxUser>(a => a.Id == entity.RecipientId).ConfigureAwait(false);
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
        }

        private void SetBtxUser(BtxMessage entity, UnitOfWork work)
        {
            if (string.IsNullOrWhiteSpace(entity.RecipientId))
                return;

            var user = work.GenericRepository.Get<BtxUser>(a => a.Id == entity.RecipientId).FirstOrDefault();
            
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
        }

    }
}
