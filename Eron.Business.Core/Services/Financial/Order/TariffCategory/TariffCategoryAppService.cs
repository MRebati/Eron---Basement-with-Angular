using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;
using Eron.Core.AppEnums;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.FileHelper;
using Eron.SharedKernel.Helpers.Mapper;
using Eron.SharedKernel.Helpers.StringExtensions;

namespace Eron.Business.Core.Services.Financial.Order.TariffCategory
{
    public class TariffCategoryAppService : ManagementSystemService, ITariffCategoryAppService
    {
        private readonly IFileHelper _fileHelper;

        public TariffCategoryAppService(
            IManagementUnitOfWork unitOfWork,
            IFileHelper fileHelper,
            TenantType tenantType = TenantType.WebService
            ) : base(unitOfWork, tenantType)
        {
            _fileHelper = fileHelper;
        }

        #region Query

        public async Task<List<TariffCategoryDto>> GetAll()
        {
            var result = await UnitOfWork.TariffCategoryRepository.GetAllAsync();
            return result.MapTo<List<TariffCategoryDto>>();
        }

        public async Task<TariffCategoryDto> GetById(int id)
        {
            var result = await UnitOfWork.TariffCategoryRepository.GetByIdAsync(id);
            return result.MapTo<TariffCategoryDto>();
        }

        public async Task<List<TariffCategoryDto>> GetTree()
        {
            var entities = await UnitOfWork.TariffCategoryRepository.GetAsync(x => x.ParentId.HasValue);
            var result = entities.MapTo<List<TariffCategoryDto>>();
            return result;
        }

        public async Task<List<TariffCategoryDto>> GetFullCategories()
        {
            var entities = await UnitOfWork.TariffCategoryRepository.GetFullCategories();
            var result = entities.MapTo<List<TariffCategoryDto>>();
            return result;
        }

        public async Task<List<TariffCategoryDto>> GetPromoted()
        {
            var entities =
                await UnitOfWork.TariffCategoryRepository.GetAsync(x => x.Promoted);
            var result = entities.MapTo<List<TariffCategoryDto>>();
            return result;
        }

        public async Task<List<TariffCategoryDto>> GetHomePage()
        {
            var entities =
                await UnitOfWork.TariffCategoryRepository.GetAsync(x => x.ViewOnHomePage);
            var result = entities.MapTo<List<TariffCategoryDto>>();
            return result;
        }

        #endregion Query

        #region Command

        public async Task<TariffCategoryDto> Create(TariffCategoryCreateOrUpdateDto input)
        {
            if (input.ImageId != null)
            {
                var eronFile = await _fileHelper.GetFileAsync(input.ImageId.Value);
                await _fileHelper.TransferToDatabaseAsync(eronFile);
            }

            var entityFromDatabase = await UnitOfWork.TariffCategoryRepository.GetByIdAsync(input.Id);
            var entity = input.MapTo<Eron.Core.Entities.Financial.Order.TariffCategory>();
            entity.Slug = entity.Slug.Slugify();
            Mapper.Map(entity, entityFromDatabase);

            entity = UnitOfWork.TariffCategoryRepository.Create(entityFromDatabase);
            await UnitOfWork.SaveAsync();
            var result = entity.MapTo<TariffCategoryDto>();
            return result;
        }

        public async Task<TariffCategoryDto> Update(TariffCategoryCreateOrUpdateDto input)
        {
            var entity = input.MapTo<Eron.Core.Entities.Financial.Order.TariffCategory>();
            if (input.ImageId.HasValue && input.ImageId != entity.ImageId)
            {
                if (entity.ImageId.HasValue)
                    await _fileHelper.DeleteFileAsync(entity.ImageId.Value);
                var newFile = await _fileHelper.GetFileAsync(input.ImageId.Value);
                await _fileHelper.TransferToDatabaseAsync(newFile);
            }
            UnitOfWork.TariffCategoryRepository.Update(entity);
            await UnitOfWork.SaveAsync();
            var result = entity.MapTo<TariffCategoryDto>();
            return result;
        }

        public async Task<bool> DeleteAsync(int inputId)
        {
            UnitOfWork.TariffCategoryRepository.DeleteLogically(inputId);
            var tariffs = await UnitOfWork.TariffRepository.GetAsync(x => x.TariffCategoryId == inputId);
            foreach (var item in tariffs)
            {
                UnitOfWork.TariffRepository.DeleteLogically(item);
            }
            await UnitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<TariffCategoryReOrderDto>> ReOrder(List<TariffCategoryReOrderDto> input)
        {
            var entities = (await UnitOfWork.TariffCategoryRepository.GetAllAsync()).ToList();

            var parents = entities.Where(x => input.Select(y => y.Id).Any(z => z == x.Id));

            foreach (var item in parents)
            {
                item.ParentId = null;
                AddChildren(item, entities, input);
                UnitOfWork.TariffCategoryRepository.Update(item);
            }

            await UnitOfWork.SaveAsync();
            return input;
        }

        #endregion Command

        #region helpers

        private void AddChildren(Eron.Core.Entities.Financial.Order.TariffCategory parent,
            List<Eron.Core.Entities.Financial.Order.TariffCategory> collection, List<TariffCategoryReOrderDto> childMap)
        {
            var newChildMap = childMap.FirstOrDefault(x => x.Id == parent.Id)?.Children;
            if (newChildMap != null)
                foreach (var item in newChildMap)
                {
                    var childEntity = collection.FirstOrDefault(x => x.Id == item.Id);
                    if (childEntity != null)
                        childEntity.ParentId = parent.Id;
                    if (item.HasChildren)
                        AddChildren(childEntity, collection, newChildMap);
                }
        }

        #endregion
    }
}
