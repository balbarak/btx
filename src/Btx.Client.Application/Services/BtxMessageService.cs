using Btx.Client.Application.Persistance;
using Btx.Client.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
