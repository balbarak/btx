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
        public BtxThread AddOrUpdate(BtxThread entity)
        {
            using (BtxDbContext context = new BtxDbContext())
            {
                context.Add(entity);

                context.SaveChanges();
            }

            return entity;
        }



        public List<BtxThread> GetAll()
        {
            List<BtxThread> threads = new List<BtxThread>();

            using (BtxDbContext context = new BtxDbContext())
            {
                threads = context.BtxThreads.ToList();
            }

            return threads;
        }
    }
}
