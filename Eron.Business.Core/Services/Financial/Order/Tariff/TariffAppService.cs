using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.FileHelper;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Business.Core.Services.Financial.Order.Tariff
{
    public class TariffAppService : ManagementSystemService, ITariffAppService
    {
        private IFileHelper _fileHelper;
        public TariffAppService(
            IManagementUnitOfWork unitOfWork,
            IFileHelper fileHelper
            ) : base(unitOfWork)
        {
            _fileHelper = fileHelper;
        }

        #region Query

        public async Task<PagedListResult<TariffDto>> GetTariffList(IPagedListRequest<Eron.Core.Entities.Financial.Order.Tariff> input)
        {
            var result = (await UnitOfWork.TariffRepository.GetAsPagedListAsync(input)).MapTo<PagedListResult<TariffDto>>();
            var tariffPriceList = await UnitOfWork.TariffPriceRepository.GetAllValidAsync();
            foreach (var item in result.Result)
            {
                item.TariffPrice = tariffPriceList.FirstOrDefault(x => x.TariffId == item.Id)?.Price;
            }
            return result;
        }

        public async Task<List<TariffDto>> GetAll()
        {
            var list = (await UnitOfWork.TariffRepository.GetAllAsync()).MapTo<List<TariffDto>>();
            var tariffPriceList = await UnitOfWork.TariffPriceRepository.GetAllValidAsync();

            foreach (var item in list)
            {
                item.TariffPrice = tariffPriceList.FirstOrDefault(x => x.TariffId == item.Id)?.Price;
            }

            return list;
        }

        public TariffDto Get(long id)
        {
            var item = UnitOfWork.TariffRepository.GetById(id);
            return item.MapTo<TariffDto>();
        }

        public async Task<TariffDto> GetAsync(long id)
        {
            var item = await UnitOfWork.TariffRepository.GetByIdAsync(id);
            var tariffItems = await UnitOfWork.TariffItemRepository.GetAllWithTariffIdAsync(item.Id);
            item.TariffItems = tariffItems;
            var result = item.MapTo<TariffDto>();
            result.TariffPrice = UnitOfWork.TariffPriceRepository.GetValidByTariffId(item.Id).Price;
            return result;
        }

        public async Task<List<TariffDto>> GetByCategoryAsync(int categoryId)
        {
            var entityList = await UnitOfWork.TariffRepository.GetQueryable(x => x.TariffCategoryId == categoryId).ToListAsync();
            var dtoList = entityList.MapTo<List<TariffDto>>();
            var tariffPriceList = await UnitOfWork.TariffPriceRepository.GetAllValidAsync();
            var tariffItems = await UnitOfWork.TariffItemRepository.GetAllWithTariffIdsAsync(entityList.Select(x => x.Id).ToList());
            var tariffItemDtos = tariffItems.MapTo<List<TariffItemDto>>();
            foreach (var item in dtoList)
            {
                item.TariffPrice = tariffPriceList.FirstOrDefault(x => x.TariffId == item.Id)?.Price;
                item.TariffItems = tariffItemDtos.Where(x => x.TariffId == item.Id).ToList();
            }
            return dtoList;
        }

        #endregion

        #region Command

        public async Task<TariffDto> Create(TariffCreateOrUpdateDto input)
        {
            using (var unitOfWork = UnitOfWork.Begin())
            {
                foreach (var item in input.TariffItems)
                {
                    item.CreateDateTime = DateTime.Now;
                }

                //map tariff base
                var tariff = input.MapTo<Eron.Core.Entities.Financial.Order.Tariff>();

                tariff.CustomerType = CustomerType.EndUserCustomer;
                if (tariff.ImageId.HasValue)
                {
                    var image = await _fileHelper.GetFileAsync(tariff.ImageId.Value);
                    await _fileHelper.TransferToDatabaseAsync(image);
                }

                //create tariff base
                var tariffCreated = UnitOfWork.TariffRepository.Create(tariff);
                await UnitOfWork.SaveAsync();

                //handle price
                var tariffPrice = new Eron.Core.Entities.Financial.Order.TariffPrice().Create(input.TariffPrice, tariffCreated.Id);
                UnitOfWork.TariffPriceRepository.Create(tariffPrice);

                tariffCreated.TariffPrices = new List<Eron.Core.Entities.Financial.Order.TariffPrice>() { tariffPrice };

                ////handle tariff items
                //foreach (var item in input.TariffItems)
                //{
                //    var tariffItem = item.MapTo<TariffItem>();
                //    tariffItem.TariffId = tariffCreated.Id;
                //    var tariffItemCreated = UnitOfWork.TariffItemRepository.Create(tariffItem);

                //    tariffCreated.TariffItems.Add(tariffItemCreated);
                //}

                //Save all dependent entites to database
                await UnitOfWork.SaveAsync();

                //Complete the transaction
                unitOfWork.Complete();
                return tariffCreated.MapTo<TariffDto>();
            }
        }

        public async Task<TariffDto> Update(TariffCreateOrUpdateDto input)
        {
            if (!input.IsUpdateEntry())
                throw new NoNullAllowedException("item does not exist or could not be found");

            //var existingItems = UnitOfWork.TariffItemRepository.GetAllWithTariffId(input.Id.Value);
            var newItems = input.TariffItems.MapTo<List<TariffItem>>();
            var tariffEntityFromDatabase = await UnitOfWork.TariffRepository.GetByIdAsync(input.Id);
            var tariff = input.MapTo<Eron.Core.Entities.Financial.Order.Tariff>();

            #region Handle Items

            ////delete old items which do not exist any more
            //foreach (var item in existingItems.Where(x => !newItems.Contains(x)))
            //{
            //    UnitOfWork.TariffItemRepository.Delete(item);
            //}

            ////add new Items which did not exist before
            //foreach (var item in newItems.Where(x => !existingItems.Contains(x)))
            //{
            //    item.TariffId = input.Id.Value;
            //    UnitOfWork.TariffItemRepository.Create(item);
            //}

            #endregion

            #region Handle Price

            //fetch existing valid price
            var existingPrice = UnitOfWork.TariffPriceRepository.GetValidByTariffId(input.Id.Value);
            //check if current price is up to date
            var newPrice = new Eron.Core.Entities.Financial.Order.TariffPrice();
            if (existingPrice.Price != input.TariffPrice)
            {
                existingPrice.IsValid = false;

                //update old tariff price
                UnitOfWork.TariffPriceRepository.Update(existingPrice);

                //create new tariff price
                newPrice = new Eron.Core.Entities.Financial.Order.TariffPrice().Create(input.TariffPrice, input.Id.Value);
                newPrice = UnitOfWork.TariffPriceRepository.Create(newPrice);
                await UnitOfWork.SaveAsync();
            }

            #endregion

            #region Handle Images

            if (tariff.ImageId.HasValue && tariff.ImageId != tariffEntityFromDatabase.ImageId)
            {
                var newImage = await _fileHelper.GetFileAsync(tariff.ImageId.Value);
                if (tariffEntityFromDatabase.ImageId.HasValue)
                    await _fileHelper.DeleteFileAsync(tariffEntityFromDatabase.ImageId.Value);
                await _fileHelper.TransferToDatabaseAsync(newImage);
            }

            #endregion

            var tariffPriceList = UnitOfWork.TariffPriceRepository.GetAllWithTariffId(tariff.Id);

            tariff.TariffPrices = tariffPriceList;
            Mapper.Map(tariff, tariffEntityFromDatabase);

            UnitOfWork.TariffRepository.Update(tariffEntityFromDatabase);

            await UnitOfWork.SaveAsync();
            return input.MapTo<TariffDto>();
        }

        public async Task<bool> Delete(long id)
        {
            var items = UnitOfWork.TariffItemRepository.GetAllWithTariffId(id);
            var prices = UnitOfWork.TariffPriceRepository.GetAllWithTariffId(id);
            foreach (var item in items)
                UnitOfWork.TariffItemRepository.DeleteLogically(item);

            foreach (var price in prices)
                UnitOfWork.TariffPriceRepository.DeleteLogically(price);

            UnitOfWork.TariffRepository.DeleteLogically(id);

            await UnitOfWork.SaveAsync();

            return true;
        }

        public async Task<bool> Delete(TariffDto input)
        {
            var items = UnitOfWork.TariffItemRepository.GetAllWithTariffId(input.Id);
            var prices = UnitOfWork.TariffPriceRepository.GetAllWithTariffId(input.Id);
            foreach (var item in items)
                UnitOfWork.TariffItemRepository.DeleteLogically(item);

            foreach (var price in prices)
                UnitOfWork.TariffPriceRepository.DeleteLogically(price);

            UnitOfWork.TariffRepository.DeleteLogically(input.Id);

            await UnitOfWork.SaveAsync();

            return true;
        }

        #endregion
    }
}
