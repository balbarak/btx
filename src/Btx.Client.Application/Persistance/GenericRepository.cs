using Btx.Client.Domain.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Btx.Client.Application.Persistance
{
    public class GenericRepository
    {
        private BtxDbContext _context;

        private string _filePath;

        public GenericRepository()
        {

        }

        public GenericRepository(BtxDbContext context)
        {
            _context = context;
        }

        public GenericRepository(string filePath)
        {
            _filePath = filePath;
        }

        public virtual async Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            await dbSet.AddAsync(entity).ConfigureAwait(false);

            if (_context == null)
            {
                await context.SaveChangesAsync().ConfigureAwait(false);

                context.Dispose();
            }

            return entity;

        }

        public virtual async  Task<SearchResult<TEntity>> Search<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;
            
            if (searchCriteria.FilterExpression != null)
            {
                query = query.Where(searchCriteria.FilterExpression);
            }

            foreach (var includeProperty in includes)
            {
                query = query.Include(includeProperty);
            }

            if (searchCriteria.SortExpression != null)
            {
                query = searchCriteria.SortExpression(query);
            }

            SearchResult<TEntity> result = new SearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = await query.CountAsync().ConfigureAwait(false),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = await query.ToListAsync().ConfigureAwait(false);


            if (_context == null)
                context.Dispose();


            return result;

        }

        public virtual async Task<List<TEntity>> GetAsync<TEntity>(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string[] includeProperties = null) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }


            var result = await query.ToListAsync().ConfigureAwait(false);

            if (_context == null)
                context.Dispose();

            return result;
        }
    }
}
