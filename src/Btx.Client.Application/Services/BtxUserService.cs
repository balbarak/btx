using Btx.Client.Application.Persistance;
using Btx.Client.Domain.Models;
using Btx.Client.Domain.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Btx.Client.Application.Services
{
    public class BtxUserService : ServiceBase<BtxUserService>
    {
        public BtxUser AddOrUpdate(BtxUser entity)
        {
            using (UnitOfWork work = new UnitOfWork())
            {
                var found = work.GenericRepository.Get<BtxUser>(a => a.Id == entity.Id).FirstOrDefault();

                if (found == null)
                {
                    work.GenericRepository.Create(entity);
                }
                else
                {
                    found = found.Update(entity);
                }

                work.Commit();
            }

            return entity;
        }

        public SearchResult<BtxUser> Search(SearchCriteria<BtxUser> search)
        {
            return _repository.Search(search);
        }
        
    }
}
