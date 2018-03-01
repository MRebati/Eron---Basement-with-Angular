using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Infrastructure
{
    public interface IAsyncCrudAppService<in TPrimaryKey, TEntity, TEntityDto, in TEntryEntityDto, in TEntityListDto> 
        : IApplicationService
        where TEntityDto: IEntityDto 
        where TEntityListDto: IPagedListRequest<TEntity> 
        where TEntryEntityDto: IEntityEntryDto
    {
        #region Query

        Task<List<TEntityDto>> GetAll();

        Task<TEntityDto> GetById(TPrimaryKey id);

        Task<List<TEntityDto>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null);

        Task<IPagedListResult<TEntityDto>> GetAllAsPagedList(TEntityListDto input);

        #endregion Query

        #region Command

        Task<TEntityDto> Create(TEntryEntityDto input);

        Task<TEntityDto> Update(TEntryEntityDto input);

        Task<bool> Delete(TPrimaryKey id);

        Task<bool> Delete(TEntityDto entity);


        Task<bool> DeleteLogically(TPrimaryKey id);

        Task<bool> DeleteLogically(TEntityDto entity);

        #endregion Command
    }
}
