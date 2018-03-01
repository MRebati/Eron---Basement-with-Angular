using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eron.Core.Exceptions;
using Eron.Core.Infrastructure;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Business.Core.Infrastructure
{
    public class AsyncCrudAppService<TPrimaryKey, TEntity, TEntityDto, TEntryEntityDto, TEntityListDto> :
        IAsyncCrudAppService<TPrimaryKey, TEntity, TEntityDto, TEntryEntityDto, TEntityListDto>
        where TEntity : IEntity
        where TEntityDto : IEntityDto
        where TEntityListDto : IPagedListRequest<TEntity>
        where TEntryEntityDto : IEntityEntryDto
    {
        #region Props

        private readonly IRepository<TEntity> _repository;

        #endregion Props

        #region Ctor

        public AsyncCrudAppService(
            IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #endregion Ctor

        #region Query

        public async Task<List<TEntityDto>> GetAll()
        {
            var result = await _repository.GetAllAsync();
            return result.MapTo<List<TEntityDto>>();
        }

        public async Task<TEntityDto> GetById(TPrimaryKey id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result.MapTo<TEntityDto>();
        }

        public async Task<List<TEntityDto>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
        {
            var result = await _repository.GetAsync(filter, orderBy, includeProperties, skip, take);
            return result.MapTo<List<TEntityDto>>();
        }

        public async Task<IPagedListResult<TEntityDto>> GetAllAsPagedList(TEntityListDto input)
        {
            var result = await _repository.GetAsPagedListAsync(input, null, input.Includes);
            return result.MapTo<IPagedListResult<TEntityDto>>();
        }

        #endregion

        #region Command

        public async Task<TEntityDto> Create(TEntryEntityDto input)
        {
            var entity = input.MapTo<TEntity>();
            var result = _repository.Create(entity);
            await _repository.SaveAsync();
            return result.MapTo<TEntityDto>();
        }

        public async Task<TEntityDto> Update(TEntryEntityDto input)
        {
            if (!input.IsUpdateEntry())
            {
                throw new EntityNotFoundException();
            }
            var entity = input.MapTo<TEntity>();
            _repository.Update(entity);
            await _repository.SaveAsync();
            return entity.MapTo<TEntityDto>();
        }

        public async Task<bool> Delete(TPrimaryKey id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> Delete(TEntityDto entity)
        {
            _repository.Delete(entity);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteLogically(TPrimaryKey id)
        {
            _repository.DeleteLogically(id);
            await _repository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteLogically(TEntityDto entity)
        {
            _repository.DeleteLogically(entity);
            await _repository.SaveAsync();
            return true;
        }

        #endregion
    }
}