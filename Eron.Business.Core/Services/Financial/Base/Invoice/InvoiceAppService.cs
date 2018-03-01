using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Base.Invoice.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Exceptions;
using Eron.Core.Infrastructure;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Expression;
using Eron.SharedKernel.Helpers.Mapper;
using Eron.SharedKernel.Helpers.PersianCalendar;
using Eron.SharedKernel.Helpers.StringExtensions;
using Microsoft.AspNet.Identity;

namespace Eron.Business.Core.Services.Financial.Base.Invoice
{
    public class InvoiceAppService : ManagementSystemService, IInvoiceAppService
    {
        public InvoiceAppService(
            IManagementUnitOfWork unitOfWork,
            TenantType tenantType = TenantType.WebService
            ) : base(unitOfWork, tenantType)
        {
        }

        #region Query

        public async Task<List<InvoiceDto>> GetUserInvoices(string userId)
        {
            var entityList = (await UnitOfWork.InvoiceRepository.GetAsync(
                x => x.UserId == userId, x => x.OrderByDescending(y => y.CreateDateTime),
                "InvoiceItems, InvoiceItems.ProductPrice, InvoiceItems.ProductPrice.Product, User"
            )).ToList();

            foreach (var invoice in entityList)
            {
                foreach (var item in invoice.InvoiceItems)
                {
                    if (item.ProductPrice != null)
                        invoice.Amount += item.ProductPrice.Price * item.Count;
                    if (item.TariffPrice != null)
                        invoice.Amount += item.TariffPrice.Price * item.Count;
                }
            }

            var result = entityList.MapTo<List<InvoiceDto>>();
            return result;
        }

        public async Task<InvoiceDto> GetUserInvoice(string invoiceNumber)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var invoice = await UnitOfWork.InvoiceRepository.GetOneAsync(
                x => x.InvoiceNumber.ToLower() == invoiceNumber.ToLower(),
                "InvoiceItems," +
                "InvoiceItems.ProductPrice, " +
                "InvoiceItems.TariffPrice, " +
                "InvoiceItems.TariffPrice.Tariff, " +
                "InvoiceItems.ProductPrice.Product, " +
                "User");

            if (invoice.UserId != userId)
                throw new UnauthorizedAccessException();

            foreach (var item in invoice.InvoiceItems)
            {
                if (item.ProductPrice != null)
                    invoice.Amount += item.ProductPrice.Price * item.Count;
                if(item.TariffPrice != null)
                    invoice.Amount += item.TariffPrice.Price * item.Count;
            }

            return invoice.MapTo<InvoiceDto>();
        }

        public async Task<InvoiceDto> GetInvoiceByNumber(string invoiceNumber)
        {
            var invoice = await UnitOfWork.InvoiceRepository
                .GetOneAsync(x =>
                x.InvoiceNumber.ToLower() == invoiceNumber.ToLower(),
                    "InvoiceItems, " +
                    "InvoiceItems.ProductPrice, " +
                    "InvoiceItems.TariffPrice, " +
                    "InvoiceItems.TariffPrice.Tariff, " +
                    "InvoiceItems.ProductPrice.Product, " +
                    "User");

            foreach (var item in invoice.InvoiceItems)
            {
                invoice.Amount += item.ProductPrice.Price * item.Count;
            }

            return invoice.MapTo<InvoiceDto>();
        }

        public async Task<List<InvoiceDto>> GetAllInvoices()
        {
            var entityList = await UnitOfWork.InvoiceRepository.GetAllAsync();
            var result = entityList.MapTo<List<InvoiceDto>>();
            return result;
        }

        public async Task<PagedListResult<InvoiceDto>> GetAllProductInvoicesAsPagedList(InvoiceListRequestDto input)
        {
            Expression<Func<Eron.Core.Entities.Financial.Base.Invoice, bool>> filter =
                (x => x.InvoiceItems.All(y => y.CartItemId.HasValue));

            #region set invoiceNumber filter

            if (input.InvoiceNumber.IsPopulated())
            {
                filter = filter.AndAlso(invoice => invoice.InvoiceNumber.Contains(input.InvoiceNumber));
            }

            #endregion set invoiceNumber filter

            #region set time period filter

            DateTime periodDateTime;
            switch (input.DatePeriod)
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
                    throw new ArgumentOutOfRangeException(nameof(input.DatePeriod), input.DatePeriod, null);
            }

            if (periodDateTime != default(DateTime))
            {
                Expression<Func<Eron.Core.Entities.Financial.Base.Invoice, bool>> periodDatetimeFilter = x => x.CreateDateTime > periodDateTime;
                filter = filter.AndAlso(periodDatetimeFilter);
            }

            #endregion set time period filter

            #region set invoiceStatusType filter

            if (input.InvoiceStatus.HasValue)
            {
                Expression<Func<Eron.Core.Entities.Financial.Base.Invoice, bool>> invoiceStatusFilter = x => x.InvoiceStatus == input.InvoiceStatus;
                filter = filter.AndAlso(invoiceStatusFilter);
            }

            #endregion set invoiceStatusType filter

            var result = await UnitOfWork.InvoiceRepository.GetAsPagedListAsync(input, filter,
                "InvoiceItems, InvoiceItems.ProductPrice");

            foreach (var invoice in result.Result)
            {
                foreach (var item in invoice.InvoiceItems)
                {
                    invoice.Amount += item.ProductPrice.Price * item.Count;
                    invoice.TotalCount += item.Count;
                }
            }

            return result.MapTo<PagedListResult<InvoiceDto>>();
        }

        public async Task<PagedListResult<InvoiceDto>> GetAllInvoicesAsPagedList(InvoiceListRequestDto input)
        {

            Expression<Func<Eron.Core.Entities.Financial.Base.Invoice, bool>> filter = x => true;

            #region set Filters

            #region set invoiceNumber filter

            if (input.InvoiceNumber.IsPopulated())
            {
                filter = filter.AndAlso(invoice => invoice.InvoiceNumber.Contains(input.InvoiceNumber));
            }

            #endregion set invoiceNumber filter

            #region set time period filter

            if (input.DatePeriod.HasValue)
            {
                DateTime periodDateTime;
                switch (input.DatePeriod)
                {
                    case DatePeriodType.Day:
                        periodDateTime = DateTime.Now.Date;
                        break;
                    case DatePeriodType.Week:
                        var dayOfWeek = PersianDateTime.Now.PersianDayOfWeek;
                        periodDateTime = DateTime.Now.Date.AddDays(-(int)dayOfWeek);
                        break;
                    case DatePeriodType.Month:
                        periodDateTime = new PersianDateTime(PersianDateTime.Now.Year, PersianDateTime.Now.Month, 1)
                            .ToDateTime();
                        break;
                    case DatePeriodType.Year:
                        periodDateTime = new PersianDateTime(PersianDateTime.Now.Year, 1, 1).ToDateTime();
                        break;
                    case DatePeriodType.All:
                        periodDateTime = default(DateTime);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(input.DatePeriod), input.DatePeriod, null);
                }

                if (periodDateTime != default(DateTime))
                    filter = filter.AndAlso(x => x.CreateDateTime > periodDateTime);
            }

            #endregion set time period filter

            #region set invoiceStatusType filter

            if (input.InvoiceStatus.HasValue)
            {
                filter = filter.AndAlso(x => x.InvoiceStatus == input.InvoiceStatus);
            }

            #endregion set invoiceStatusType filter

            #region set invoiceType filter

            if (input.Type.HasValue)
            {
                filter = filter.AndAlso(x => x.Type == input.Type);
            }

            #endregion

            #endregion

            #region set Order

            input.SetOrder("CreateDateTime", "desc");

            #endregion

            var result = await UnitOfWork.InvoiceRepository.GetAsPagedListAsync(input, filter,
                "InvoiceItems, InvoiceItems.ProductPrice, InvoiceItems.TariffPrice, User");

            foreach (var invoice in result.Result)
            {
                foreach (var item in invoice.InvoiceItems)
                {
                    if (item.ProductPrice != null)
                        invoice.Amount += item.ProductPrice.Price * item.Count;
                    else if (item.TariffPrice != null)
                        invoice.Amount += item.TariffPrice.Price * item.Count;
                    invoice.TotalCount += item.Count;
                }
            }

            return result.MapTo<PagedListResult<InvoiceDto>>();
        }

        #endregion Query

        #region Command

        public async Task<InvoiceDto> CreateInvoice(InvoiceCreateOrUpdateDto input)
        {
            var entity = input.MapTo<Eron.Core.Entities.Financial.Base.Invoice>();

            var entityCreated = UnitOfWork.InvoiceRepository.Create(entity);
            await UnitOfWork.SaveAsync();
            return entityCreated.MapTo<InvoiceDto>();
        }

        public async Task<InvoiceDto> CreateInvoiceByOrders(List<string> orderNumbers)
        {
            var orders = await UnitOfWork.OrderRepository.GetOrderListByNumberListAsync(orderNumbers);
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var invoice = new Eron.Core.Entities.Financial.Base.Invoice()
            {
                ExpireDateTime = DateTime.Now.AddDays(1),
                InvoiceNumber = GenerateNewInvoiceId(),
                UserId = userId,
                InvoiceStatus = InvoiceStatusType.WaitingForPayment,
                Type = InvoiceType.Order
            };

            invoice.CheckProgress();

            foreach (var item in orders.Where(x => x.OrderStatus == OrderStatusType.NeedInvoice))
            {
                var invoiceItem = new InvoiceItem()
                {
                    Count = 1,
                    Description = item.Description,
                    OrderId = item.Id,
                    TariffPriceId = UnitOfWork.TariffPriceRepository.GetValidByTariffId(item.TariffId).Id
                };

                invoice.InvoiceItems.Add(invoiceItem);
            }

            using (var unitOfWork = UnitOfWork.Begin())
            {
                var entityCreated = UnitOfWork.InvoiceRepository.Create(invoice);
                await UnitOfWork.OrderRepository.ChangeStateOfOrderListAsync(
                    orderNumbers,
                    OrderStatusType.NeedInvoice,
                    OrderStatusType.WaitingForPayment,
                    userId);
                await UnitOfWork.SaveAsync();

                var result = entityCreated.MapTo<InvoiceDto>();
                unitOfWork.Complete();
                return result;
            }
        }

        public async Task<InvoiceDto> CreateInvoiceByCart()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var cartItems = await UnitOfWork.CartRepository.GetUserUnpaidCartList(userId);

            var invoice = new Eron.Core.Entities.Financial.Base.Invoice()
            {
                ExpireDateTime = DateTime.Now.AddDays(1),
                InvoiceNumber = GenerateNewInvoiceId(),
                UserId = userId,
                InvoiceStatus = InvoiceStatusType.WaitingForPayment,
                Type = InvoiceType.Cart
            };

            invoice.CheckProgress();

            foreach (var item in cartItems)
            {
                var invoiceItem = new InvoiceItem()
                {
                    Count = item.Count,
                    CartItemId = item.Id,
                    ProductPriceId = (await UnitOfWork.ProductPriceRepository.GetActiveForProduct(item.ProductId)).Id
                };

                invoice.InvoiceItems.Add(invoiceItem);
            }

            var entityCreated = UnitOfWork.InvoiceRepository.Create(invoice);
            await UnitOfWork.SaveAsync();

            var result = entityCreated.MapTo<InvoiceDto>();
            return result;
        }

        public async Task<List<InvoiceDto>> ChangeStateOfInvoiceList(InvoiceChangeStatusDto input, string userId)
        {
            var invoiceList = await UnitOfWork.InvoiceRepository.GetByNumberListAsync(input.Invoices);
            foreach (var item in invoiceList)
            {

                InvoiceLog log = new InvoiceLog()
                {
                    Id = Guid.NewGuid(),
                    FromState = item.InvoiceStatus,
                    ToState = input.InvoiceStatus,
                    Description = input.Description,
                    UserId = userId
                };

                item.InvoiceStatus = input.InvoiceStatus;
                UnitOfWork.InvoiceRepository.Update(item);
                UnitOfWork.InvoiceLogRepository.Create(log);
            }

            await UnitOfWork.SaveAsync();
            return invoiceList.MapTo<List<InvoiceDto>>();
        }

        #region Helper

        private string GenerateNewInvoiceId()
        {
            var dateTime = PersianDateTime.Now.Date.ToShortDateInt();
            var salt = Guid.NewGuid().ToString("N").Substring(0, 3).ToUpper();
            return $"CH-{dateTime}-IN-{salt}";
        }

        #endregion Helper

        #endregion Command
    }
}
