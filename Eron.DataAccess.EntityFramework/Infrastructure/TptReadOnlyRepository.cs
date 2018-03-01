using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eron.Core.Infrastructure;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Infrastructure
{
    public class TptReadOnlyRepository<TEntityBase, DrivedType> : ITptReadOnlyRepository<TEntityBase, DrivedType>
        where TEntityBase : class, IEntity
    {
        protected readonly DbContext Context;

        public TptReadOnlyRepository(DbContext context)
        {
            this.Context = context;
        }

        protected virtual IQueryable<DrivedType> GetQueryable(
            Expression<Func<DrivedType, bool>> filter = null,
            Func<IQueryable<DrivedType>, IOrderedQueryable<DrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<DrivedType> query = Context.Set<TEntityBase>().OfType<DrivedType>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        public virtual IEnumerable<DrivedType> GetAll(
            Func<IQueryable<DrivedType>, IOrderedQueryable<DrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(null, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<DrivedType>> GetAllAsync(
            Func<IQueryable<DrivedType>, IOrderedQueryable<DrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(null, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual IEnumerable<DrivedType> Get(
            Expression<Func<DrivedType, bool>> filter = null,
            Func<IQueryable<DrivedType>, IOrderedQueryable<DrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<DrivedType>> GetAsync(
            Expression<Func<DrivedType, bool>> filter = null,
            Func<IQueryable<DrivedType>, IOrderedQueryable<DrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual DrivedType GetOne(
            Expression<Func<DrivedType, bool>> filter = null,
            string includeProperties = "")
        {
            return GetQueryable(filter, null, includeProperties).SingleOrDefault();
        }

        public virtual async Task<DrivedType> GetOneAsync(
            Expression<Func<DrivedType, bool>> filter = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, null, includeProperties).SingleOrDefaultAsync();
        }

        public virtual DrivedType GetFirst(
            Expression<Func<DrivedType, bool>> filter = null,
            Func<IQueryable<DrivedType>, IOrderedQueryable<DrivedType>> orderBy = null,
            string includeProperties = "")
        {
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public virtual async Task<DrivedType> GetFirstAsync(
            Expression<Func<DrivedType, bool>> filter = null,
            Func<IQueryable<DrivedType>, IOrderedQueryable<DrivedType>> orderBy = null,
            string includeProperties = null)
        {
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual int GetCount(Expression<Func<DrivedType, bool>> filter = null)
        {
            return GetQueryable(filter).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<DrivedType, bool>> filter = null)
        {
            return GetQueryable(filter).CountAsync();
        }

        public virtual bool GetExists(Expression<Func<DrivedType, bool>> filter = null)
        {
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<DrivedType, bool>> filter = null)
        {
            return GetQueryable(filter).AnyAsync();
        }



    }
}