using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Exceptions;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;
using Eron.SharedKernel.Helpers.StringExtensions;

namespace Eron.Business.Core.Services.Financial.Shop.ProductCategory
{
    public class ProductCategoryAppService : ManagementSystemService, IProductCategoryAppService
    {
        public ProductCategoryAppService(IManagementUnitOfWork unitOfWork,
            TenantType tenantType = TenantType.WebService) : base(unitOfWork, tenantType)
        {
        }

        #region Query

        public async Task<List<ProductCategoryDto>> GetAll()
        {
            var entityList = await UnitOfWork.ProductCategoryRepository.GetAllAsync();
            foreach (var item in entityList)
            {
                item.Products = item.Products.Where(x => !x.IsDeleted).ToList();
            }
            return entityList.MapTo<List<ProductCategoryDto>>();
        }

        public async Task<ProductCategoryDto> GetById(int id)
        {
            var entity = await UnitOfWork.ProductCategoryRepository.GetByIdAsync(id);
            entity.Products = entity.Products.Where(x => !x.IsDeleted).ToList();
            return entity.MapTo<ProductCategoryDto>();
        }

        public async Task<List<ProductCategoryDto>> GetTree()
        {
            var entities = (await UnitOfWork.ProductCategoryRepository.GetAllAsync()).ToList();
            foreach (var item in entities)
            {
                item.Products = item.Products.Where(x => !x.IsDeleted).ToList();
            }

            var parents = entities.Where(x => x.ParentId == null).MapTo<List<ProductCategoryDto>>();
            //foreach (var parent in parents)
            //    AddChildren(parent, entities);
            return parents;
        }

        public async Task<List<ProductCategoryDto>> GetFullCategories()
        {
            var entityList = await UnitOfWork.ProductCategoryRepository.GetFullCategories();
            foreach (var item in entityList)
            {
                item.Products = item.Products.Where(x => !x.IsDeleted).ToList();
            }
            var result = entityList.MapTo<List<ProductCategoryDto>>();
            return result;
        }

        public async Task<List<ProductCategoryDto>> GetPromoted()
        {
            var entityList = await UnitOfWork.ProductCategoryRepository.GetAsync(x => x.Promoted);
            foreach (var item in entityList)
            {
                item.Products = item.Products.Where(x => !x.IsDeleted).ToList();
            }
            var result = entityList.MapTo<List<ProductCategoryDto>>();
            return result;
        }

        public async Task<List<ProductCategoryDto>> GetHomePage()
        {
            var entityList = await UnitOfWork.ProductCategoryRepository.GetAsync(x => x.ViewOnHomePage);
            foreach (var item in entityList)
            {
                item.Products = item.Products.Where(x => !x.IsDeleted).ToList();
            }
            var result = entityList.MapTo<List<ProductCategoryDto>>();
            foreach (var item in result)
            {
                var prices = await UnitOfWork.ProductPriceRepository.GetActiveForProducts(item.Products.Select(x => x.Id).ToList());
                foreach (var product in item.Products)
                {
                    var productPrice = prices.FirstOrDefault(x => x.ProductId == product.Id);
                    if (productPrice != null)
                        product.Price = productPrice.Price;
                }
            }

            return result;
        }

        #endregion

        #region Command

        public async Task<ProductCategoryDto> Create(ProductCategoryCreateOrUpdateDto input)
        {
            var entity = input.MapTo<Eron.Core.Entities.Financial.Shop.ProductCategory>();
            entity.Slug = entity.Slug.IsPopulated() ? entity.Slug.Slugify() : entity.Title.Slugify();
            var result = UnitOfWork.ProductCategoryRepository.Create(entity);
            await UnitOfWork.SaveAsync();
            return result.MapTo<ProductCategoryDto>();
        }

        public async Task<ProductCategoryDto> Update(ProductCategoryCreateOrUpdateDto input)
        {
            if (!input.IsUpdateEntry())
                throw new EntityNotFoundException();

            var entity = input.MapTo<Eron.Core.Entities.Financial.Shop.ProductCategory>();
            UnitOfWork.ProductCategoryRepository.Update(entity);
            await UnitOfWork.SaveAsync();
            return entity.MapTo<ProductCategoryDto>();
        }

        public async Task<bool> Delete(int inputId)
        {
            var entityExists = await UnitOfWork.ProductCategoryRepository.GetExistsAsync(x => x.Id == inputId);
            if (!entityExists)
                return false;
            UnitOfWork.ProductCategoryRepository.DeleteLogically(inputId);

            var products = await UnitOfWork.ProductRepository.GetAsync(x => x.CategoryId == inputId);
            foreach (var product in products)
                UnitOfWork.ProductRepository.DeleteLogically(product);

            var children = await UnitOfWork.ProductCategoryRepository.GetAsync(x => x.ParentId == inputId);
            foreach (var child in children)
            {
                await Delete(child.Id);
            }

            await UnitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<ProductCategoryReOrderDto>> ReOrder(List<ProductCategoryReOrderDto> input)
        {
            var entities = (await UnitOfWork.ProductCategoryRepository.GetAllAsync()).ToList();

            var parents = entities.Where(x => input.Select(y => y.Id).Any(z => z == x.Id));

            foreach (var item in parents)
            {
                item.ParentId = null;
                AddChildren(item, entities, input);
                UnitOfWork.ProductCategoryRepository.Update(item);
            }

            await UnitOfWork.SaveAsync();
            return input;
        }

        #region helpers

        private void AddChildren(Eron.Core.Entities.Financial.Shop.ProductCategory parent,
            List<Eron.Core.Entities.Financial.Shop.ProductCategory> collection, List<ProductCategoryReOrderDto> childMap)
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

        #endregion
    }
}