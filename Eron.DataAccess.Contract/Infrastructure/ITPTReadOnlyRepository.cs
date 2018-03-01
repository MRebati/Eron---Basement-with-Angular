using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eron.DataAccess.Contract.Infrastructure
{
    public interface ITptReadOnlyRepository<TEntityBase, TDrivedType>
    {
        IEnumerable<TDrivedType> GetAll(
            Func<IQueryable<TDrivedType>, IOrderedQueryable<TDrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<TDrivedType>> GetAllAsync(
            Func<IQueryable<TDrivedType>, IOrderedQueryable<TDrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        IEnumerable<TDrivedType> Get(
            Expression<Func<TDrivedType, bool>> filter = null,
            Func<IQueryable<TDrivedType>, IOrderedQueryable<TDrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IEnumerable<TDrivedType>> GetAsync(
            Expression<Func<TDrivedType, bool>> filter = null,
            Func<IQueryable<TDrivedType>, IOrderedQueryable<TDrivedType>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        TDrivedType GetOne(
            Expression<Func<TDrivedType, bool>> filter = null,
            string includeProperties = null);

        Task<TDrivedType> GetOneAsync(
            Expression<Func<TDrivedType, bool>> filter = null,
            string includeProperties = null);

        TDrivedType GetFirst(
            Expression<Func<TDrivedType, bool>> filter = null,
            Func<IQueryable<TDrivedType>, IOrderedQueryable<TDrivedType>> orderBy = null,
            string includeProperties = null);

        Task<TDrivedType> GetFirstAsync(
            Expression<Func<TDrivedType, bool>> filter = null,
            Func<IQueryable<TDrivedType>, IOrderedQueryable<TDrivedType>> orderBy = null,
            string includeProperties = null);

        int GetCount(Expression<Func<TDrivedType, bool>> filter = null);

        Task<int> GetCountAsync(Expression<Func<TDrivedType, bool>> filter = null);

        bool GetExists(Expression<Func<TDrivedType, bool>> filter = null);

        Task<bool> GetExistsAsync(Expression<Func<TDrivedType, bool>> filter = null);
    }
}