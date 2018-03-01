using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Order.Dto;
using Eron.Core.AppEnums;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.FileHelper;
using Eron.SharedKernel.Helpers.Mapper;
using Eron.SharedKernel.Helpers.PersianCalendar;
using Microsoft.AspNet.Identity;

namespace Eron.Business.Core.Services.Financial.Order.Order
{
    public class OrderAppService : ManagementSystemService, IOrderAppService
    {
        private readonly IFileHelper _fileHelper;

        public OrderAppService(
            IManagementUnitOfWork unitOfWork,
            IFileHelper fileHelper,
            TenantType tenantType = TenantType.WebService
        ) : base(unitOfWork, tenantType)
        {
            _fileHelper = fileHelper;
        }

        #region Query

        public async Task<List<OrderDto>> GetUserOrders()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var result = await UnitOfWork.OrderRepository.GetOrders(x => x.UserId == userId);
            return result.MapTo<List<OrderDto>>();
        }

        public async Task<OrderDto> GetById(Guid id)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var isInAdminRole = HttpContext.Current.User.IsInRole("admin");

            var result = await UnitOfWork.OrderRepository.GetOrderById(id);

            if (!isInAdminRole && result.UserId != userId)
            {
                throw new UnauthorizedAccessException();
            }
            return result.MapTo<OrderDto>();
        }

        public async Task<OrderDto> GetByNumber(string orderNumber)
        {
            var entity = await UnitOfWork.OrderRepository.GetOrderByNumberAsync(orderNumber);
            var result = entity.MapTo<OrderDto>();
            result.ImageIds = entity.Images.Select(x => x.Id.ToString()).ToList();
            return result.MapTo<OrderDto>();
        }

        public async Task<List<OrderDto>> GetAllUsersOrders()
        {
            var result = await UnitOfWork.OrderRepository.GetOrders();
            return result.MapTo<List<OrderDto>>();
        }

        public async Task<PagedListResult<OrderDto>> GetAllUsersOrdersAsPagedList(OrderListRequestDto input)
        {
            var result = await UnitOfWork.OrderRepository.GetOrdersAsPagedList(
                null,
                input.OrderNumber,
                input.PageNumber,
                input.PageSize,
                input.OrderStatus,
                input.DatePeriod);

            var finalResult = result.MapTo<PagedListResult<OrderDto>>();
            return finalResult;
        }

        public async Task<List<OrderDto>> GetAllUsersApprovedOrders()
        {
            var result = await UnitOfWork.OrderRepository.GetOrders(x => x.Approved);
            return result.MapTo<List<OrderDto>>();
        }

        public async Task<List<OrderDto>> GetAllUsersUnpaidOrders()
        {
            var orderInvoiceItems =
                (await UnitOfWork.InvoiceItemRepository.GetAsync(x => x.OrderId != null)).Select(x => x.OrderId);
            var result = await UnitOfWork.OrderRepository.GetOrders(x => orderInvoiceItems.Contains(x.Id));
            return result.MapTo<List<OrderDto>>();
        }

        public async Task<List<OrderDto>> GetDesignPricePendingOrders()
        {
            var result = await UnitOfWork.OrderRepository.GetOrders(x => x.HasDesignOrder && x.DesignPrice == 0);
            return result.MapTo<List<OrderDto>>();
        }

        #endregion Query

        #region Command

        public async Task<OrderDto> CreateOrder(OrderCreateOrUpdateDto input)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var entity = input.MapTo<Eron.Core.Entities.Financial.Order.Order>();

            entity.UserId = userId;
            entity.OrderNumber = GenerateOrderNumber();
            entity.OrderStatus = OrderStatusType.NeedInvoice;
            entity.Id = Guid.NewGuid();

            //if (entity.HasDesignOrder)
            //{
            //    entity.DesignPrice = entity.Tariff.DesignPrice.HasValue ?
            //        entity.Tariff.DesignPrice.Value : 0;
            //}

            UnitOfWork.OrderRepository.Create(entity);
            await UnitOfWork.SaveAsync();

            foreach (var imageId in input.ImageIds)
            {
                var eronFile = await _fileHelper.GetFileAsync(imageId);
                await _fileHelper.TransferToDatabaseAsync(eronFile);
                eronFile.OrderId = entity.Id;
                UnitOfWork.FileRepository.Update(eronFile);
                await UnitOfWork.SaveAsync();
            }

            return entity.MapTo<OrderDto>();
        }

        public async Task<OrderDto> ApproveOrder(Guid orderId)
        {
            var entity = await UnitOfWork.OrderRepository.GetByIdAsync(orderId);
            entity.Approved = true;
            entity.OrderStatus = OrderStatusType.NeedInvoice;
            UnitOfWork.OrderRepository.Update(entity);
            await UnitOfWork.SaveAsync();

            var currentUserId = HttpContext.Current.User.Identity.GetUserId();
            var activeTariffPrice = UnitOfWork.TariffPriceRepository.GetValidByTariffId(entity.TariffId);

            //var cartItem = new CartItem()
            //{
            //    Count = entity.Count,
            //    OrderId = entity.Id,
            //    OrderPrice = activeTariffPrice.Price,
            //    OrderNumber = entity.OrderNumber,
            //    OrderImage = entity.Images.Any() ? entity.Images.FirstOrDefault().Id.ToString() : "",
            //    UserId = currentUserId
            //};

            //UnitOfWork.CartRepository.Create(cartItem);
            //await UnitOfWork.SaveAsync();

            return entity.MapTo<OrderDto>();
        }

        public async Task<OrderDto> AssignOrderDesignPrice(OrderDesignPriceDto input)
        {
            var entity = UnitOfWork.OrderRepository.GetById(input.Id);
            entity.DesignPrice = input.DesignPrice;
            entity.OrderStatus = OrderStatusType.NeedInvoice;
            UnitOfWork.OrderRepository.Update(entity);
            await UnitOfWork.SaveAsync();
            return entity.MapTo<OrderDto>();
        }

        public async Task<bool> CancelOrderByOrderNumberAsUser(List<string> orderNumbers)
        {
            var entityList =
                await UnitOfWork.OrderRepository.GetAsync(x => orderNumbers.Select(y => y.ToLower()).Contains(x.OrderNumber.ToLower()));

            foreach (var item in entityList)
            {
                item.OrderStatus = OrderStatusType.Canceled;
                UnitOfWork.OrderRepository.Update(item);
            }

            await UnitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> ChangeStateOfOrderList(OrderChangeStatusDto input, string userId)
        {
            await UnitOfWork.OrderRepository.ChangeStateOfOrderListForceAsync(
                input.Orders,
                input.OrderStatus,
                userId,
                input.Description);

            await UnitOfWork.SaveAsync();
            return true;
        }

        #region Helpers

        private string GenerateOrderNumber()
        {
            var salt = Guid.NewGuid().ToString("N").Substring(0, 3).ToUpper();

            var datetime = PersianDateTime.Today.Date.ToShortDateInt();
            return $"CH-{datetime}-OR-{salt}";
        }

        #endregion Helpers

        #endregion Command
    }
}