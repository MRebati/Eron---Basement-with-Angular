using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.User;
using Eron.Core.Exceptions;
using Eron.Core.ManagementSettings;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Order;
using Eron.DataAccess.EntityFramework.Infrastructure;
using Eron.SharedKernel.Helpers.PersianCalendar;
using Eron.SharedKernel.Helpers.StringExtensions;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Order
{
    public class OrderRepository : Repository<Core.Entities.Financial.Order.Order>, IOrderRepository
    {
        private readonly IRepository<Tariff> _tariffRepository;
        private readonly IRepository<TariffPrice> _tariffPriceRepository;
        private readonly IRepository<TariffItem> _tariffItemRepository;
        private readonly IRepository<OrderLog> _orderLogRepository;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<InvoiceItem> _invoiceItemRepository;
        private readonly IRepository<EronFile> _fileRepository;


        public OrderRepository(
            DbContext context,
            IRepository<Tariff> tariffRepository,
            IRepository<TariffPrice> tariffPriceRepository,
            IRepository<TariffItem> tariffItemRepository,
            IRepository<OrderLog> orderLogRepository,
            IRepository<Invoice> invoiceRepository,
            IRepository<InvoiceItem> invoiceItemRepository,
            IRepository<EronFile> fileRepository) : base(context)
        {
            _tariffRepository = tariffRepository;
            _tariffPriceRepository = tariffPriceRepository;
            _tariffItemRepository = tariffItemRepository;
            _orderLogRepository = orderLogRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _fileRepository = fileRepository;
        }

        #region Query

        public async Task<List<Core.Entities.Financial.Order.Order>> GetOrders(Expression<Func<Core.Entities.Financial.Order.Order, bool>> predicate = null)
        {
            var result = await (from order in GetQueryable()
                                join tariff in _tariffRepository.GetQueryable() on order.TariffId equals tariff.Id
                                join tariffPrice in _tariffPriceRepository.GetQueryable() on tariff.Id equals tariffPrice.TariffId
                                join tariffItem in _tariffItemRepository.GetQueryable() on tariff.Id equals tariffItem.TariffId
                                join invoiceItemNullable in _invoiceItemRepository.GetQueryable() on order.Id equals invoiceItemNullable.OrderId into invoiceItemList
                                from invoiceItem in invoiceItemList.DefaultIfEmpty()
                                join invoiceNullable in _invoiceRepository.GetQueryable() on invoiceItem.InvoiceId equals invoiceNullable.Id into invoiceList
                                from invoice in invoiceList.DefaultIfEmpty()
                                orderby order.CreateDateTime descending
                                select new { order, tariff, tariffItem, tariffPrice, invoice }).ToListAsync();

            foreach (var item in result)
            {
                var firstOrDefault = result.Select(x => x.tariffPrice)
                    .FirstOrDefault(x => x.TariffId == item.order.TariffId && x.IsValid);
                if (firstOrDefault != null)
                    item.order.Price = firstOrDefault.Price;

                item.order.TariffName = item.tariff.TariffName;
                var quantityExists = result.Select(x => x.tariffItem)
                    .FirstOrDefault(x => x.Type == TariffItemType.Quantity);
                if (quantityExists != null)
                    item.order.Quantity = Int64.Parse(quantityExists.Name) * item.order.Count;
                else
                    item.order.Quantity = 1;

                item.order.InvoiceNumber = item.invoice?.InvoiceNumber;
                item.order.InvoiceId = item.invoice?.Id;
                item.order.FinalPrice = item.order.Price * item.order.Count + item.order.DesignPrice;
            }

            var finalResult = result.Select(x => x.order).AsQueryable();

            if (predicate != null)
                finalResult = finalResult.Where(predicate);

            return finalResult.Distinct().OrderByDescending(x => x.CreateDateTime).ToList();
        }

        public async Task<Core.Entities.Financial.Order.Order> GetOrderById(Guid id)
        {
            var order = await GetByIdAsync(id);

            var orderDetails = await (from tariff in _tariffRepository.GetQueryable()
                                      join tariffPrice in _tariffPriceRepository.GetQueryable() on tariff.Id equals tariffPrice.TariffId
                                      join tariffItemNullable in _tariffItemRepository.GetQueryable() on tariff.Id equals tariffItemNullable.TariffId into tariffItemList
                                      from tariffItem in tariffItemList.DefaultIfEmpty()
                                      join fileNullable in _fileRepository.GetQueryable() on order.Id equals fileNullable.OrderId into fileList
                                      from file in fileList.DefaultIfEmpty()

                                      where tariff.Id == order.TariffId
                                      where tariffPrice.IsValid
                                      let quantity = tariffItem != null && tariffItem.Type == TariffItemType.Quantity ? tariffItem.Name : "1"
                                      select new
                                      {
                                          tariffName = tariff.TariffName,
                                          price = tariffPrice.Price,
                                          quantity,
                                          file
                                      }).ToListAsync();

            order.Price = orderDetails.FirstOrDefault()?.price ?? 0;
            order.TariffName = orderDetails.FirstOrDefault()?.tariffName;
            order.Quantity = Int64.Parse(orderDetails.FirstOrDefault()?.quantity ?? "1") * order.Count;
            order.Images = orderDetails.Select(x => x.file).Distinct().ToList();
            order.FinalPrice = order.Count * order.Price + order.DesignPrice;
            var applicationUser = await context.Set<ApplicationUser>().FindAsync(order.UserId);
            order.User = applicationUser;

            return order;
        }

        public async Task<Core.Entities.Financial.Order.Order> GetOrderByNumberAsync(string orderNumber)
        {
            var orderId = (await GetOneAsync(x => x.OrderNumber.ToLower() == orderNumber.ToLower())).Id;
            return await this.GetOrderById(orderId);
        }

        public async Task<List<Core.Entities.Financial.Order.Order>> GetOrderListByNumberListAsync(List<string> orderNumberList)
        {
            var orders = await GetAsync(x => orderNumberList.Select(y => y.ToLower()).Contains(x.OrderNumber.ToLower()));
            return orders.ToList();
        }

        public async Task<PagedListResult<Core.Entities.Financial.Order.Order>> GetOrdersAsPagedList(
            Expression<Func<Core.Entities.Financial.Order.Order, bool>> predicate,
            string orderNumber,
            int pageNumber = 0,
            int pageSize = ApplicationSettings.Pagination.PageSize,
            OrderStatusType? orderStatus = null,
            DatePeriodType periodType = DatePeriodType.All
        )
        {
            #region Pagination Set

            var skipCount = (pageNumber - 1) * pageSize;

            #endregion

            #region set time period filter

            DateTime periodDateTime;
            switch (periodType)
            {
                case DatePeriodType.Day:
                    periodDateTime = DateTime.Now.Date;
                    break;
                case DatePeriodType.Week:
                    var dayOfWeek = PersianDateTime.Now.PersianDayOfWeek;
                    periodDateTime = DateTime.Now.Date.AddDays(-(int)dayOfWeek);
                    break;
                case DatePeriodType.Month:
                    periodDateTime = new PersianDateTime(PersianDateTime.Now.Year, PersianDateTime.Now.Month, 1).ToDateTime();
                    break;
                case DatePeriodType.Year:
                    periodDateTime = new PersianDateTime(PersianDateTime.Now.Year, 1, 1).ToDateTime();
                    break;
                case DatePeriodType.All:
                    periodDateTime = default(DateTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(periodType), periodType, null);
            }

            #endregion set time period filter

            var resultCount = await (from order in GetQueryable()
                                     join tariff in _tariffRepository.GetQueryable() on order.TariffId equals tariff.Id
                                     join tariffPrice in _tariffPriceRepository.GetQueryable() on tariff.Id equals tariffPrice.TariffId
                                     join tariffItem in _tariffItemRepository.GetQueryable() on tariff.Id equals tariffItem.TariffId
                                     where orderStatus == null || order.OrderStatus == orderStatus
                                     where periodDateTime == default(DateTime) || order.CreateDateTime > periodDateTime
                                     where orderNumber == null || orderNumber == "" || order.OrderNumber.Contains(orderNumber)
                                     orderby order.CreateDateTime descending
                                     select order
            ).Distinct().CountAsync();

            var result = await (from order in GetQueryable()
                                join tariff in _tariffRepository.GetQueryable() on order.TariffId equals tariff.Id
                                join tariffPrice in _tariffPriceRepository.GetQueryable() on tariff.Id equals tariffPrice.TariffId
                                join tariffItem in _tariffItemRepository.GetQueryable() on tariff.Id equals tariffItem.TariffId
                                where orderStatus == null || order.OrderStatus == orderStatus
                                where periodDateTime == default(DateTime) || order.CreateDateTime > periodDateTime
                                where orderNumber == null || orderNumber == "" || order.OrderNumber.Contains(orderNumber)
                                orderby order.CreateDateTime descending
                                select new { order, tariff, tariffItem, tariffPrice }
            ).ToListAsync();

            foreach (var item in result)
            {
                var firstOrDefault = result.Select(x => x.tariffPrice)
                    .FirstOrDefault(x => x.TariffId == item.order.TariffId && x.IsValid);
                if (firstOrDefault != null)
                    item.order.Price = firstOrDefault.Price;

                item.order.TariffName = item.tariff.TariffName;
                var quantityExists = result.Select(x => x.tariffItem)
                    .FirstOrDefault(x => x.Type == TariffItemType.Quantity);
                if (quantityExists != null)
                    item.order.Quantity = Int64.Parse(quantityExists.Name) * item.order.Count;
                else
                    item.order.Quantity = 1;

                item.order.FinalPrice = item.order.Price * item.order.Count + item.order.DesignPrice;
            }

            var finalResult = result.Select(x => x.order).AsQueryable();

            if (predicate != null)
                finalResult = finalResult.Where(predicate);

            var listItems = finalResult.Distinct().Skip(skipCount).Take(pageSize).ToList();
            return new PagedListResult<Core.Entities.Financial.Order.Order>()
            {
                PageNumber = pageNumber,
                Result = listItems,
                PageSize = pageSize,
                TotalCount = resultCount
            };
        }

        #endregion Query

        #region Command

        public async Task<Core.Entities.Financial.Order.Order> ChangeStateOfOrderAsync(string orderNumber, OrderStatusType oldOrderStatus, OrderStatusType newOrderStatus, string userId, string description = null)
        {
            var entity = await GetOrderByNumberAsync(orderNumber);
            if (entity.OrderStatus == oldOrderStatus)
            {
                CreateLog(entity, newOrderStatus, userId);
                entity.OrderStatus = newOrderStatus;
                Update(entity);
                return entity;
            }

            return entity;
        }

        public async Task<Core.Entities.Financial.Order.Order> ChangeStateOfOrderForceAsync(string orderNumber, OrderStatusType newOrderStatus, string userId, string description = null)
        {
            var entity = await GetOrderByNumberAsync(orderNumber);
            CreateLog(entity, newOrderStatus, userId);
            entity.OrderStatus = newOrderStatus;
            Update(entity);
            return entity;
        }

        public async Task<List<Core.Entities.Financial.Order.Order>> ChangeStateOfOrderListAsync(List<string> orderNumber, OrderStatusType oldOrderStatus, OrderStatusType newOrderStatus, string userId, string description = null)
        {
            var entityList = await GetOrderListByNumberListAsync(orderNumber);
            foreach (var entity in entityList)
            {
                if (entity.OrderStatus == oldOrderStatus)
                {
                    CreateLog(entity, newOrderStatus, userId);
                    entity.OrderStatus = newOrderStatus;
                    Update(entity);
                }
            }

            return entityList;
        }

        public async Task<List<Core.Entities.Financial.Order.Order>> ChangeStateOfOrderListForceAsync(List<string> orderNumber, OrderStatusType newOrderStatus, string userId, string description = null)
        {
            var entityList = await GetOrderListByNumberListAsync(orderNumber);
            foreach (var entity in entityList)
            {
                entity.OrderStatus = newOrderStatus;
                CreateLog(entity, newOrderStatus, userId);
                Update(entity);
            }

            return entityList;
        }

        private void CreateLog(Core.Entities.Financial.Order.Order entity, OrderStatusType newOrderStatus, string userId, string description = null)
        {
            OrderLog logEntity = new OrderLog()
            {
                Id = Guid.NewGuid(),
                FromState = entity.OrderStatus,
                ToState = newOrderStatus,
                Description = description,
                UserId = userId
            };
            this._orderLogRepository.Create(logEntity);
        }

        #endregion Command
    }
}
