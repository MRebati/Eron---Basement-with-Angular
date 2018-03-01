﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eron.Core.Infrastructure;
using Eron.Core.ManagementSettings;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.SharedKernel.Helpers.Expression;
using Eron.SharedKernel.Helpers.ListExtensions;
using Eron.SharedKernel.Helpers.Mapper;
using Eron.SharedKernel.Helpers.StringExtensions;

namespace Eron.DataAccess.EntityFramework.Infrastructure
{
    public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
    where TEntity : class, IEntity
    {
        protected readonly DbContext context;

        public ReadOnlyRepository(DbContext context)
        {
            this.context = context;
        }

        public virtual IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? skip = null,
        int? take = null)
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = context.Set<TEntity>();

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
                if (orderBy == null)
                    query = query.OrderBy("Id ASC").Skip(skip.Value);
                else
                    query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                if (orderBy == null)
                    query = query.OrderBy("Id ASC").Take(take.Value);
                else
                    query = query.Take(take.Value);
            }

            return query;
        }

        public virtual IEnumerable<TEntity> GetAll(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return GetQueryable(x => !x.IsDeleted, orderBy, includeProperties, skip, take).ToList();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            return await GetQueryable(x => !x.IsDeleted, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            AddInterceptFilters(filter, out filter);
            return GetQueryable(filter, orderBy, includeProperties, skip, take).ToList();
        }

        public IPagedListResult<TEntity> GetAsPagedList(IPagedListRequest<TEntity> pagedListRequest, Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            //pageNumber is 1 indexed. so We for the first items to fetch we need to compute it from zero.
            var skip = pagedListRequest.PageSize * (pagedListRequest.PageNumber - 1);
            var take = pagedListRequest.PageSize;

            pagedListRequest.SetFilters(filter, out filter);
            AddInterceptFilters(filter, out filter);
            var count = GetQueryable(filter).Count();

            if (pagedListRequest.Order.IsPopulated())
            {
                return GetQueryable(filter, null, includeProperties).OrderBy(pagedListRequest.Order).Skip(skip).Take(take).ToPagedList(count);
            }

            return GetQueryable(filter, null, includeProperties, skip, take).ToPagedList(count);
        }

        public async Task<IPagedListResult<TEntity>> GetAsPagedListAsync(IPagedListRequest<TEntity> pagedListRequest, Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            if (filter != null)
                pagedListRequest.SetFilters(filter, out filter);
            AddInterceptFilters(filter, out filter);
            if (pagedListRequest.PageNumber == 0)
                pagedListRequest.PageNumber = 1;

            if (pagedListRequest.PageSize == 0)
                pagedListRequest.PageSize = ApplicationSettings.Pagination.PageSize;
            //pageNumber is 1 indexed. so We for the first items to fetch we need to compute it from zero.
            var skip = pagedListRequest.PageSize * (pagedListRequest.PageNumber - 1);
            var take = pagedListRequest.PageSize;

            var count = await GetQueryable(filter).CountAsync();

            if (pagedListRequest.Order.IsPopulated())
            {
                return await GetQueryable(filter, null, includeProperties, skip, take).OrderBy(pagedListRequest.Order).ToPagedListAsync(count);
            }

            return await GetQueryable(filter, null, includeProperties, skip, take).ToPagedListAsync(count);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            AddInterceptFilters(filter, out filter);
            return await GetQueryable(filter, orderBy, includeProperties, skip, take).ToListAsync();
        }

        public virtual TEntity GetOne(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
        {
            AddInterceptFilters(filter, out filter);
            return GetQueryable(filter, null, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetOneAsync(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
        {
            AddInterceptFilters(filter, out filter);
            return await GetQueryable(filter, null, includeProperties).FirstOrDefaultAsync();
        }

        public virtual TEntity GetFirst(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            AddInterceptFilters(filter, out filter);
            return GetQueryable(filter, orderBy, includeProperties).FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
        {
            AddInterceptFilters(filter, out filter);
            return await GetQueryable(filter, orderBy, includeProperties).FirstOrDefaultAsync();
        }

        public virtual TEntity GetById(object id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public virtual Task<TEntity> GetByIdAsync(object id)
        {
            return context.Set<TEntity>().FindAsync(id);
        }

        public virtual int GetCount(Expression<Func<TEntity, bool>> filter = null)
        {
            AddInterceptFilters(filter, out filter);
            return GetQueryable(filter).Count();
        }

        public virtual Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            AddInterceptFilters(filter, out filter);
            return GetQueryable(filter).CountAsync();
        }

        public virtual bool GetExists(Expression<Func<TEntity, bool>> filter = null)
        {
            AddInterceptFilters(filter, out filter);
            return GetQueryable(filter).Any();
        }

        public virtual Task<bool> GetExistsAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            AddInterceptFilters(filter, out filter);
            return GetQueryable(filter).AnyAsync();
        }

        #region Helpers

        private void AddInterceptFilters(Expression<Func<TEntity, bool>> filter, out Expression<Func<TEntity, bool>> expression)
        {
            if (filter == null)
                expression = x => !x.IsDeleted;
            else
                expression = filter.AndAlso(x => !x.IsDeleted);
        }

        #endregion

    }
}
