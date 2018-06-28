﻿using Btx.Client.Application.Persistance;
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
                nameof(BtxMessage.Recipient)
            };
        }
        
        public async Task<BtxMessage> Add(BtxMessage entity)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
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

                entity = await work.GenericRepository.CreateAsync(entity).ConfigureAwait(false);

                await work.CommitAsync().ConfigureAwait(false);
            }

            return entity;
        }
        

        public async Task<SearchResult<BtxMessage>> Search(SearchCriteria<BtxMessage> search)
        {
            return await _repository.Search(search,Includes);
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
