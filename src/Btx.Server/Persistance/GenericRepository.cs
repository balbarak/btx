﻿using Btx.Client.Domain.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Btx.Server.Persistance
{
    public class GenericRepository
    {
        internal BtxDbContext _context;

        public GenericRepository() { }

        public GenericRepository(BtxDbContext context)
        {
            this._context = context;

        }

        public virtual List<TEntity> Create<TEntity>(List<TEntity> entites) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();
            
            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entites;
        }

        public virtual TEntity Create<TEntity>(TEntity entity) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();
            
            dbSet.Add(entity);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entity;
        }

        public async virtual Task<List<TEntity>> CreateAsync<TEntity>(List<TEntity> entites) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();
            
            await dbSet.AddRangeAsync(entites);

            if (_context == null)
            {
                await context.SaveChangesAsync();
                context.Dispose();
            }

            return entites;
        }

        public async virtual Task<TEntity> CreateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();
            
            await dbSet.AddAsync(entity);

            if (_context == null)
            {
                await context.SaveChangesAsync();
                context.Dispose();
            }

            return entity;
        }

        public virtual TEntity Update<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();
            
            context.Update<TEntity>(entityToUpdate);
            
            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }

            return entityToUpdate;
        }

        public async virtual Task<TEntity> UpdateAsync<TEntity>(TEntity entityToUpdate) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();
            
            context.Update(entityToUpdate);

            if (_context == null)
            {
                await context.SaveChangesAsync();
                context.Dispose();
            }

            return entityToUpdate;
        }

        public virtual void Delete<TEntity>(TEntity entityToDelete) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();


            var dbSet = context.Set<TEntity>();

            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }

            dbSet.Remove(entityToDelete);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }

        public virtual void Delete<TEntity>(object id) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            var found = dbSet.Find(id);

            dbSet.Remove(found);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }

        public virtual void Delete<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            var items = dbSet.Where(filter);

            dbSet.RemoveRange(items);

            if (_context == null)
            {
                context.SaveChanges();
                context.Dispose();
            }
        }

        public async virtual Task DeleteAsync<TEntity>(object id) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            var found = await dbSet.FindAsync(id);

            dbSet.Remove(found);

            if (_context == null)
            {
                await context.SaveChangesAsync();
                context.Dispose();
            }
        }

        public async virtual Task<int> CountAsync<TEntity>() where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            int count;

            count = await dbSet.CountAsync();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public async virtual Task<int> CountAsync<TEntity>(SearchCriteria<TEntity> search) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            int count = 0;

            if (search.FilterExpression != null)
            {
                query = query.Where(search.FilterExpression);
            }

            count = await query.CountAsync();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual int Count<TEntity>() where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            int count;

            count = dbSet.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual int Count<TEntity>(SearchCriteria<TEntity> search) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            var dbSet = context.Set<TEntity>();

            IQueryable<TEntity> query = dbSet;

            int count = 0;

            if (search.FilterExpression != null)
            {
                query = query.Where(search.FilterExpression);
            }

            count = query.Count();

            if (_context == null)
                context.Dispose();

            return count;
        }

        public virtual SearchResult<TEntity> Search<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class
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
            else
            {
                //query = query.OrderByDescending(entity => entity.ID);
            }

            SearchResult<TEntity> result = new SearchResult<TEntity>(searchCriteria)
            {
                TotalResultsCount = query.Count(),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = query.ToList();


            if (_context == null)
                context.Dispose();


            return result;

        }

        public async virtual Task<SearchResult<TEntity>> SearchAsync<TEntity>(SearchCriteria<TEntity> searchCriteria, params string[] includes) where TEntity : class
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
                TotalResultsCount = await query.CountAsync(),
            };

            query = query.Skip(searchCriteria.StartIndex).Take(searchCriteria.PageSize);

            result.Result = await query.ToListAsync();


            if (_context == null)
                context.Dispose();


            return result;

        }

        public virtual TEntity GetByID<TEntity>(params object[] keys) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            TEntity entity = context.Set<TEntity>().Find(keys);

            return entity;

        }

        public async virtual Task<TEntity> GetByIDAsync<TEntity>(params object[] keys) where TEntity : class
        {
            BtxDbContext context = _context ?? new BtxDbContext();

            TEntity entity = await context.Set<TEntity>().FindAsync(keys);

            return entity;

        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
         Expression<Func<TEntity, bool>> filter = null,
         Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string[] includeProperties = null, int? maxSize = null) where TEntity : class
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
                if (maxSize.HasValue)
                    query = orderBy(query);
                else
                    query = orderBy(query);
            }
            else
            {
                if (maxSize.HasValue)
                    query = query.Take(maxSize.Value);
            }

            var result = query.ToList();

            if (_context == null)
                context.Dispose();

            return result;
        }
    }
}
