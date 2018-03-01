using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Exceptions;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.DataAccess.EntityFramework.Repositories.Financial.Shop;
using Eron.SharedKernel.Helpers.Expression;
using Eron.SharedKernel.Helpers.FileHelper;
using Eron.SharedKernel.Helpers.Mapper;
using Eron.SharedKernel.Helpers.StringExtensions;

namespace Eron.Business.Core.Services.Financial.Shop.Product
{
    public class ProductAppService : ManagementSystemService, IProductAppService
    {
        private IFileHelper _fileHelper;
        private readonly IProductPropertyRepository _productPropertyRepository;

        public ProductAppService(
            IManagementUnitOfWork unitOfWork,
            IFileHelper fileHelper, IProductPropertyRepository productPropertyRepository, TenantType tenantType = TenantType.WebService
            ) : base(unitOfWork, tenantType)
        {
            _fileHelper = fileHelper;
            _productPropertyRepository = productPropertyRepository;
        }

        #region Query

        public async Task<PagedListResult<ProductDto>> GetAllAsPagedList(ProductListRequestDto input)
        {
            Expression<Func<Eron.Core.Entities.Financial.Shop.Product, bool>> filter = product => true;

            #region Check Filters

            if (input.ProductName.IsPopulated())
                filter = x => x.Name.ToLower().Contains(input.ProductName.ToLower());

            if (input.ProductCode.IsPopulated())
                filter = filter.AndAlso(x => x.ProductCode.ToLower().Contains(input.ProductCode.ToLower()));

            if (input.Availability.HasValue)
                filter = filter.AndAlso(x => x.ExistsInShop == input.Availability);


            #endregion

            var result = await UnitOfWork.ProductRepository.GetAsPagedListAsync(input, filter);

            foreach (var item in result.Result)
            {
                var lastPrice = item.Prices.OrderByDescending(x => x.CreateDateTime).FirstOrDefault();
                if (lastPrice != null)
                    item.Price = lastPrice.Price;
            }

            return result.MapTo<PagedListResult<ProductDto>>();
        }

        public async Task<List<ProductDto>> GetAll()
        {
            var list = await UnitOfWork.ProductRepository.GetAsync();
            foreach (var item in list)
            {
                item.Price = (await UnitOfWork.ProductPriceRepository.GetActiveForProduct(item.Id)).Price;
            }
            var result = list.MapTo<List<ProductDto>>();
            return result;
        }

        public async Task<List<ProductDto>> GetByCategoryId(int categoryId)
        {
            var modelList = (await UnitOfWork.ProductRepository.GetAsync(x => x.CategoryId == categoryId)).ToList().MapTo<List<ProductDto>>();
            var prices = await UnitOfWork.ProductPriceRepository.GetActiveForProducts(modelList.Select(x => x.Id));
            foreach (var dto in modelList)
            {
                dto.Price = prices.FirstOrDefault(x => x.ProductId == dto.Id)?.Price ?? 0;
                dto.Images = (await UnitOfWork.FileRepository.GetProductImages(dto.Id))
                    .Select(x => x.Id.ToString()).ToList();
            }
            return modelList;
        }

        public async Task<List<ProductDto>> GetPromoted()
        {
            var entities = await UnitOfWork.ProductRepository.GetPromotedProducts();
            foreach (var item in entities)
            {
                var currentPrice = await UnitOfWork.ProductPriceRepository.GetActiveForProduct(item.Id);
                if(currentPrice != null)
                item.Price = currentPrice.Price;
            }
            var result = entities.MapTo<List<ProductDto>>();
            return result;
        }

        public async Task<ProductDto> GetByProductCode(string productCode)
        {
            var entity = await UnitOfWork.ProductRepository.GetOneAsync(x => x.ProductCode == productCode, "Category");
            if (entity != null)
            {
                entity.CategoryName = entity.Category.Title;
                var price = await UnitOfWork.ProductPriceRepository.GetActiveForProduct(entity.Id);
                var propertyNameIds = entity.Properties.Select(x => x.ProductPropertyNameId);
                var propertyNames =
                    await UnitOfWork.ProductPropertyNameRepository.GetAsync(x => propertyNameIds.Contains(x.Id));
                foreach (var item in entity.Properties)
                {
                    item.Name = propertyNames.FirstOrDefault(x => x.Id == item.ProductPropertyNameId).Name;
                }

                var result = entity.MapTo<ProductDto>();
                result.Price = price.Price;
                result.Images =
                    (await UnitOfWork.FileRepository.GetProductImages(entity.Id))
                    .Select(x => x.Id.ToString()).ToList();

                return result;
            }

            throw new EntityNotFoundException();
        }

        public async Task<ProductCreateOrUpdateDto> GetByProductCodeForUpdate(string productCode)
        {
            var entity = await UnitOfWork.ProductRepository.GetOneAsync(x => x.ProductCode == productCode);
            var price = await UnitOfWork.ProductPriceRepository.GetActiveForProduct(entity.Id);

            var propertyNameIds = entity.Properties.Select(x => x.ProductPropertyNameId);
            var propertyNames = await UnitOfWork.ProductPropertyNameRepository.GetAsync(x => propertyNameIds.Contains(x.Id));
            foreach (var item in entity.Properties)
            {
                item.Name = propertyNames.FirstOrDefault(x => x.Id == item.ProductPropertyNameId).Name;
            }

            var result = entity.MapTo<ProductCreateOrUpdateDto>();
            result.ProductPrice = price.Price;
            result.Images =
                (await UnitOfWork.FileRepository.GetProductImages(entity.Id))
                .Select(x => x.Id.ToString()).ToList();

            return result;
        }

        public ProductDto GetById(long id)
        {
            var entity = UnitOfWork.ProductRepository.GetById(id);
            return entity.MapTo<ProductDto>();
        }

        public async Task<List<ProductDto>> GetRelatedProductsByProductCode(string productCode)
        {
            var mainProduct = await GetByProductCode(productCode);
            var productsInSameCategory = await GetProductsInCategory(mainProduct.CategoryId);

            foreach (var item in productsInSameCategory)
            {
                item.Price = (await UnitOfWork.ProductPriceRepository.GetActiveForProduct(item.Id)).Price;
            }

            if (productsInSameCategory.Count > 10)
            {
                var result = new List<ProductDto>();
                Random random = new Random();
                for (var i = 1; i < 11; i++)
                {
                    var randomNumber = random.Next(0, productsInSameCategory.Count);
                    result.Add(productsInSameCategory[randomNumber]);
                }

                return result;
            }
            return productsInSameCategory;

        }

        public async Task<List<ProductDto>> GetProductsInCategory(int categoryId)
        {
            var list = await UnitOfWork.ProductRepository.GetAsync(x => x.CategoryId == categoryId && x.ExistsInShop);
            foreach (var item in list)
            {
                item.Price = (await UnitOfWork.ProductPriceRepository.GetActiveForProduct(item.Id)).Price;
            }
            return list.MapTo<List<ProductDto>>();
        }

        public async Task<List<ProductDto>> GetBestSellers()
        {
            var entities = await UnitOfWork.ProductRepository.GetBestSellerProducts();

            var priceList = await UnitOfWork.ProductPriceRepository.GetActiveForProducts(entities.Select(x => x.Id).ToList());

            foreach (var entity in entities)
            {
                var currentPrice = priceList.FirstOrDefault(x => x.ProductId == entity.Id);
                if (currentPrice != null)
                entity.Price = currentPrice.Price;
            }

            var result = entities.MapTo<List<ProductDto>>();
            return result;
        }

        #endregion Query

        #region Command

        public async Task<ProductDto> Create(ProductCreateOrUpdateDto input)
        {
            using (var unitOfWork = UnitOfWork.Begin())
            {

                #region Handle Product Property Names

                var allPropertyNames = (await UnitOfWork.ProductPropertyNameRepository.GetAllAsync()).ToList();

                foreach (var item in input.Properties)
                {
                    var productPropertyNameEntity = new ProductPropertyName(item.PropertyName);
                    if (allPropertyNames.Any(x => x.Name == item.PropertyName))
                        item.ProductPropertyNameId = allPropertyNames.Find(x => x.Name == item.PropertyName).Id;
                    else
                    {
                        var newProductPropertyName = UnitOfWork.ProductPropertyNameRepository.Create(productPropertyNameEntity);
                        await UnitOfWork.SaveAsync();
                        item.ProductPropertyNameId = newProductPropertyName.Id;
                    }
                }

                #endregion

                #region Handle Main Product

                var entity = input.MapTo<Eron.Core.Entities.Financial.Shop.Product>();
                entity = UnitOfWork.ProductRepository.Create(entity);

                await UnitOfWork.SaveAsync();
                var entityId = entity.Id;

                foreach (var image in input.Images)
                {
                    var eronFile = await _fileHelper.GetFileAsync(image);
                    eronFile.ProductId = entityId;
                    await _fileHelper.TransferToDatabaseAsync(eronFile);
                }

                await UnitOfWork.SaveAsync();

                #endregion

                #region Handle Price

                var price = new Eron.Core.Entities.Financial.Shop.ProductPrice(entityId, input.ProductPrice);
                UnitOfWork.ProductPriceRepository.Create(price);
                await UnitOfWork.SaveAsync();

                #endregion

                var entityDto = entity.MapTo<ProductDto>();
                entityDto.Images = input.Images;
                entityDto.Properties = input.Properties.MapTo<List<ProductPropertyDto>>();
                entityDto.Price = input.ProductPrice;

                unitOfWork.Complete();

                return entityDto;
            }
        }

        public async Task<ProductDto> Update(ProductCreateOrUpdateDto input)
        {
            if (!input.IsUpdateEntry())
                throw new EntityNotFoundException();

            using (var unitOfWork = UnitOfWork.Begin())
            {
                #region Handle Product Property Names

                var allPropertyNames = (await UnitOfWork.ProductPropertyNameRepository.GetAllAsync()).ToList();

                foreach (var item in input.Properties)
                {
                    var productPropertyNameEntity = new ProductPropertyName(item.PropertyName);
                    if (allPropertyNames.Any(x => x.Name == item.PropertyName))
                        item.ProductPropertyNameId = allPropertyNames.Find(x => x.Name == item.PropertyName).Id;
                    else
                    {
                        var newProductPropertyName = UnitOfWork.ProductPropertyNameRepository.Create(productPropertyNameEntity);
                        await UnitOfWork.SaveAsync();
                        item.ProductPropertyNameId = newProductPropertyName.Id;
                    }

                    item.ProductId = input.Id;
                }

                #endregion

                #region Handle Main Product

                var updateEntity = input.MapTo<Eron.Core.Entities.Financial.Shop.Product>();

                #endregion

                #region Handle ProductProperties and PropertyName

                #endregion

                UnitOfWork.ProductRepository.Update(updateEntity);
                await UnitOfWork.SaveAsync();

                #region Handle Price

                var currentPrice =
                    await UnitOfWork.ProductPriceRepository.GetActiveForProduct(updateEntity.Id);

                if (currentPrice.Price != updateEntity.Price)
                {
                    currentPrice.IsValid = false;
                    var newPrice =
                        new Eron.Core.Entities.Financial.Shop.ProductPrice(updateEntity.Id, updateEntity.Price);
                    UnitOfWork.ProductPriceRepository.Update(currentPrice);
                    UnitOfWork.ProductPriceRepository.Create(newPrice);
                    await UnitOfWork.SaveAsync();
                }

                #endregion

                #region Properties

                //foreach (var item in updateEntity.Properties)
                //{
                //    var propertyEntity = item.MapTo<ProductProperty>();
                //    if (item.Id == 0)
                //    {
                //        UnitOfWork.ProductPropertyRepository.Create(propertyEntity);
                //    }
                //    else
                //    {
                //        _productPropertyRepository.Update(propertyEntity);
                //    }
                //}

                //await UnitOfWork.SaveAsync();

                #endregion Properties

                unitOfWork.Complete();

                return updateEntity.MapTo<ProductDto>();
            }
        }

        public Task<bool> Delete(ProductDto input)
        {
            #region Handle Main Product



            #endregion

            #region Handle Price



            #endregion

            #region Handle ProductProperties and PropertyName



            #endregion

            throw new NotImplementedException();
        }

        public async Task<bool> Delete(long id)
        {
            var productPrices = await UnitOfWork.ProductPriceRepository.GetAsync(x => x.ProductId == id);
            var images = await UnitOfWork.FileRepository.GetProductImages(id);
            var productProperties = await UnitOfWork.ProductPropertyRepository.GetAsync(x => x.ProductId == id);

            UnitOfWork.ProductRepository.DeleteLogically(id);
            foreach (var price in productPrices)
                UnitOfWork.ProductPriceRepository.DeleteLogically(price.Id);
            foreach (var property in productProperties)
                UnitOfWork.ProductPropertyRepository.DeleteLogically(property.Id);
            foreach (var image in images)
                UnitOfWork.FileRepository.Delete(image.Id);
            await UnitOfWork.SaveAsync();

            return true;
        }

        #endregion Command

    }
}
