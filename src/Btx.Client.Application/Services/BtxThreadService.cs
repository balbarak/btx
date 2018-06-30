using Btx.Client.Application.Persistance;
using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Btx.Client.Application.Services
{
    public class BtxThreadService : ServiceBase<BtxThreadService>
    {
        public async Task<BtxThread> AddOrUpdateAsync(BtxThread entity)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                var result = await _repository.GetAsync<BtxThread>(a => a.Id == entity.Id);

                if (result.FirstOrDefault() == null)
                {
                    entity = await work.GenericRepository.CreateAsync(entity);

                    await work.CommitAsync();
                }
            }
            
            return entity;
        }

        public BtxThread AddOrUpdate(BtxThread entity)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                var result = _repository.Get<BtxThread>(a => a.Id == entity.Id);

                if (result.FirstOrDefault() == null)
                {
                    entity = work.GenericRepository.Create(entity);

                    work.Commit();
                }
            }

            return entity;
        }

        public async Task<List<BtxThread>> GetAllAsync()
        {
            return await _repository.GetAsync<BtxThread>().ConfigureAwait(false);
        }

        public List<BtxThread> GetAll()
        {
            return _repository.Get<BtxThread>();
        }
    }
}
