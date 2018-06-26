using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Application.Persistance
{
    public class UnitOfWork : IDisposable
    {
        public readonly BtxDbContext Context = new BtxDbContext();

        private GenericRepository _genericRepository;

        public GenericRepository GenericRepository
        {
            get
            {
                if (_genericRepository == null)
                    _genericRepository = new GenericRepository(Context);

                return _genericRepository;
            }

        }

        public async Task<int> CommitAsync()
        {
            return await Context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }
    }
}
