using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Insight.Dto;
using Eron.Core.AppEnums;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.PersianCalendar;

namespace Eron.Business.Core.Services.Base.Insight
{
    public class InsightAppService : ManagementSystemService, IInsightAppService
    {
        public InsightAppService(IManagementUnitOfWork unitOfWork, TenantType tenantType = TenantType.WebService) : base(unitOfWork, tenantType)
        {
        }

        public async Task<double> GetLastMonthSales()
        {
            var current = PersianDateTime.Now;
            var lastMonth = PersianDateTime.Now.AddMonths(-1);

            var startOfCurrentMonth = new PersianDateTime(current.Year, current.Month, 1).ToDateTime();
            var startOfLastMonth = new PersianDateTime(lastMonth.Year, lastMonth.Month, 1).ToDateTime();

            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var tariffPrices = UnitOfWork.TariffPriceRepository.GetQueryable(x => !x.IsDeleted);
            var productPrices = UnitOfWork.ProductPriceRepository.GetQueryable(x => !x.IsDeleted);

            var model = await (from invoiceItem in invoiceItems
                               join tariffPriceNullable in tariffPrices on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into tariffPriceList
                               from tariffPrice in tariffPriceList.DefaultIfEmpty()
                               join productPriceNullable in productPrices on invoiceItem.ProductPriceId equals productPriceNullable.Id
                               into productPriceList
                               from productPrice in productPriceList.DefaultIfEmpty()

                               where
                               invoiceItem.CreateDateTime > startOfLastMonth &&
                               invoiceItem.CreateDateTime < startOfCurrentMonth

                               select new
                               {
                                   invoiceItem,
                                   tariffPrice = tariffPrice != null ? tariffPrice.Price : 0,
                                   productPrice = tariffPrice != null ? productPrice.Price : 0

                               }).ToListAsync();

            long totalPrice = 0;

            foreach (var item in model)
            {
                totalPrice += item.productPrice;
                totalPrice += item.tariffPrice;
            }

            return totalPrice;
        }

        public async Task<double> GetCurrentMonthSales()
        {
            var current = PersianDateTime.Now;

            var startOfCurrentMonth = new PersianDateTime(current.Year, current.Month, 1).ToDateTime();

            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var tariffPrices = UnitOfWork.TariffPriceRepository.GetQueryable(x => !x.IsDeleted);
            var productPrices = UnitOfWork.ProductPriceRepository.GetQueryable(x => !x.IsDeleted);

            var model = await (from invoiceItem in invoiceItems
                               join tariffPriceNullable in tariffPrices on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into
                               tariffPriceList
                               from tariffPrice in tariffPriceList.DefaultIfEmpty()
                               join productPriceNullable in productPrices on invoiceItem.ProductPriceId equals productPriceNullable.Id
                               into productPriceList
                               from productPrice in productPriceList.DefaultIfEmpty()

                               where invoiceItem.CreateDateTime >= startOfCurrentMonth

                               let tariffPriceValue = tariffPrice != null ? tariffPrice.Price : 0
                               let productPriceValue = productPrice != null ? productPrice.Price : 0


                               select new
                               {
                                   invoiceItem,
                                   tariffPrice = tariffPriceValue,
                                   productPrice = productPriceValue

                               }).ToListAsync();

            long totalPrice = 0;

            foreach (var item in model)
            {
                totalPrice += item.productPrice;
                totalPrice += item.tariffPrice;
            }

            return totalPrice;
        }

        public async Task<double> GetCurrentDaySales()
        {
            var currentDay = DateTime.Now.Date;

            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var tariffPrices = UnitOfWork.TariffPriceRepository.GetQueryable(x => !x.IsDeleted);
            var productPrices = UnitOfWork.ProductPriceRepository.GetQueryable(x => !x.IsDeleted);

            var model = await (from invoiceItem in invoiceItems
                               join tariffPriceNullable in tariffPrices on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into
                               tariffPriceList
                               from tariffPrice in tariffPriceList.DefaultIfEmpty()
                               join productPriceNullable in productPrices on invoiceItem.ProductPriceId equals productPriceNullable.Id
                               into productPriceList
                               from productPrice in productPriceList.DefaultIfEmpty()

                               where invoiceItem.CreateDateTime >= currentDay

                               select new
                               {
                                   invoiceItem,
                                   tariffPrice = tariffPrice != null ? tariffPrice.Price : 0,
                                   productPrice = tariffPrice != null ? productPrice.Price : 0

                               }).ToListAsync();

            long totalPrice = 0;

            foreach (var item in model)
            {
                totalPrice += item.productPrice;
                totalPrice += item.tariffPrice;
            }

            return totalPrice;
        }

        public async Task<double> GetClients()
        {
            var userCount = await UnitOfWork.AppContext.Users.CountAsync();
            return userCount;
        }

        public async Task<double> GetCustomers()
        {
            var invoiceUsers = await (from invoice in UnitOfWork.InvoiceRepository.GetQueryable(x => !x.IsDeleted)
                                      select invoice.UserId).Distinct().CountAsync();
            return invoiceUsers;
        }

        public async Task<double> GetPagesCount()
        {
            var pagesCount = await UnitOfWork.PageRepository.GetCountAsync(x => !x.IsDeleted);
            return pagesCount;
        }

        public async Task<double> GetOrdersCount()
        {
            var invoices = UnitOfWork.InvoiceRepository.GetQueryable(x => !x.IsDeleted);
            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var financialTransactions = UnitOfWork.FinanceTransactionRepository.GetQueryable(x => !x.IsDeleted);

            var results = await (from invoice in invoices
                                 join financialTransaction in financialTransactions on invoice.Id equals financialTransaction.InvoiceId
                                 join invoiceItem in invoiceItems on invoice.Id equals invoiceItem.InvoiceId
                                 where invoiceItem.TariffPriceId.HasValue
                                 where financialTransaction.Successful
                                 select invoice).Distinct().CountAsync();

            return results;
        }

        public async Task<double> GetProductsCounts()
        {
            var result = await UnitOfWork.ProductRepository.GetCountAsync(x => !x.IsDeleted && x.ExistsInShop);
            return result;
        }

        public async Task<double> GetTariffCounts()
        {
            var result = await UnitOfWork.TariffRepository.GetCountAsync(x => !x.IsDeleted);
            return result;
        }

        public async Task<double> GetLastWeekSales()
        {
            var currentDayOfWeek = (int)PersianDateTime.Now.PersianDayOfWeek;
            var startOfCurrentWeek = PersianDateTime.Now.AddDays(-currentDayOfWeek).ToDateTime();
            var startOfLastWeek = PersianDateTime.Now.AddDays(-(currentDayOfWeek + 7)).ToDateTime();

            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var tariffPrices = UnitOfWork.TariffPriceRepository.GetQueryable(x => !x.IsDeleted);
            var productPrices = UnitOfWork.ProductPriceRepository.GetQueryable(x => !x.IsDeleted);

            var model = await (from invoiceItem in invoiceItems
                join tariffPriceNullable in tariffPrices on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into tariffPriceList
                from tariffPrice in tariffPriceList.DefaultIfEmpty()
                join productPriceNullable in productPrices on invoiceItem.ProductPriceId equals productPriceNullable.Id
                into productPriceList
                from productPrice in productPriceList.DefaultIfEmpty()

                where
                invoiceItem.CreateDateTime >= startOfLastWeek &&
                invoiceItem.CreateDateTime < startOfCurrentWeek

                select new
                {
                    invoiceItem,
                    tariffPrice = tariffPrice != null ? tariffPrice.Price : 0,
                    productPrice = productPrice != null ? productPrice.Price : 0
                }).ToListAsync();

            long totalPrice = 0;

            foreach (var item in model)
            {
                totalPrice += item.productPrice;
                totalPrice += item.tariffPrice;
            }

            return totalPrice;
        }

        public async Task<double> GetCurrentWeekSales()
        {
            var currentDayOfWeek = (int)PersianDateTime.Now.PersianDayOfWeek;
            var startOfCurrentWeek = PersianDateTime.Now.AddDays(-currentDayOfWeek).ToDateTime();
            var startOfLastWeek = PersianDateTime.Now.AddDays(-(currentDayOfWeek + 7)).ToDateTime();

            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var tariffPrices = UnitOfWork.TariffPriceRepository.GetQueryable(x => !x.IsDeleted);
            var productPrices = UnitOfWork.ProductPriceRepository.GetQueryable(x => !x.IsDeleted);

            var model = await (from invoiceItem in invoiceItems
                               join tariffPriceNullable in tariffPrices on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into tariffPriceList
                               from tariffPrice in tariffPriceList.DefaultIfEmpty()
                               join productPriceNullable in productPrices on invoiceItem.ProductPriceId equals productPriceNullable.Id
                               into productPriceList
                               from productPrice in productPriceList.DefaultIfEmpty()

                               where
                               //invoiceItem.CreateDateTime >= startOfLastWeek &&
                               invoiceItem.CreateDateTime >= startOfCurrentWeek

                               select new
                               {
                                   invoiceItem,
                                   tariffPrice = tariffPrice != null ? tariffPrice.Price : 0,
                                   productPrice = productPrice != null ? productPrice.Price : 0

                               }).ToListAsync();

            long totalPrice = 0;

            foreach (var item in model)
            {
                totalPrice += item.productPrice;
                totalPrice += item.tariffPrice;
            }

            return totalPrice;
        }

        public async Task<List<InsightDto>> GetLastMonthSalesWithDayByDayDetails()
        {
            var current = PersianDateTime.Now;
            var lastMonth = PersianDateTime.Now.AddMonths(-1);

            var startOfCurrentMonth = new PersianDateTime(current.Year, current.Month, 1).ToDateTime();
            var startOfLastMonth = new PersianDateTime(lastMonth.Year, lastMonth.Month, 1).ToDateTime();

            var invoices = UnitOfWork.InvoiceRepository.GetQueryable(x => !x.IsDeleted);
            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var tariffPrices = UnitOfWork.TariffPriceRepository.GetQueryable(x => !x.IsDeleted);
            var productPrices = UnitOfWork.ProductPriceRepository.GetQueryable(x => !x.IsDeleted);
            var transactions = UnitOfWork.FinanceTransactionRepository.GetQueryable(x => !x.IsDeleted);
            var model = await (from invoiceItem in invoiceItems
                               join invoice in invoices on invoiceItem.InvoiceId equals invoice.Id
                               join transaction in transactions on invoice.Id equals transaction.InvoiceId
                               join tariffPriceNullable in tariffPrices on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into tariffPriceList
                               from tariffPrice in tariffPriceList.DefaultIfEmpty()
                               join productPriceNullable in productPrices on invoiceItem.ProductPriceId equals productPriceNullable.Id
                               into productPriceList
                               from productPrice in productPriceList.DefaultIfEmpty()

                               where
                               transaction.Successful &&
                               invoiceItem.CreateDateTime >= startOfLastMonth &&
                               invoiceItem.CreateDateTime < startOfCurrentMonth
                               let productPriceValue = productPrice != null ? productPrice.Price : 0
                               let tariffPriceValue = tariffPrice != null ? tariffPrice.Price : 0
                               where invoiceItem.CreateDateTime > startOfCurrentMonth
                               select new
                               {
                                   invoice,
                                   invoiceItem,
                                   tariffPrice = tariffPriceValue,
                                   productPrice = productPriceValue,
                               }).GroupBy(x => x.invoice).ToListAsync();

            var invoiceResult = model.Select(x => new
            {
                invoice = x.Key,
                sum = x.Sum(y => y.tariffPrice + y.productPrice)
            }).ToList();

            var resultList = new List<InsightDto>();
            foreach (var item in invoiceResult)
            {
                var monthInsightItem = new InsightDto()
                {
                    CreateDateTime = item.invoice.CreateDateTime,
                    Value = item.sum,
                    DateTime = item.invoice.CreateDateTime,
                    Id = item.invoice.CreateDateTime.Day
                };

                if (resultList.Any(x => x.Id == monthInsightItem.Id))
                {
                    var instance = resultList.FirstOrDefault(x => x.Id == monthInsightItem.Id);
                    if (instance != null) instance.Value += monthInsightItem.Value;
                }
                else
                {
                    resultList.Add(monthInsightItem);
                }
            }

            return resultList;
        }

        public async Task<List<InsightDto>> GetCurrentMonthSalesWithDayByDayDetails()
        {
            var current = PersianDateTime.Now;
            var startOfCurrentMonth = new PersianDateTime(current.Year, current.Month, 1).ToDateTime();

            var invoices = UnitOfWork.InvoiceRepository.GetQueryable(x => !x.IsDeleted);
            var invoiceItems = UnitOfWork.InvoiceItemRepository.GetQueryable(x => !x.IsDeleted);
            var tariffPrices = UnitOfWork.TariffPriceRepository.GetQueryable(x => !x.IsDeleted);
            var productPrices = UnitOfWork.ProductPriceRepository.GetQueryable(x => !x.IsDeleted);
            var transactions = UnitOfWork.FinanceTransactionRepository.GetQueryable(x => !x.IsDeleted);

            var model = await (from invoiceItem in invoiceItems
                               join invoice in invoices on invoiceItem.InvoiceId equals invoice.Id
                               join transaction in transactions on invoice.Id equals transaction.InvoiceId
                               join tariffPriceNullable in tariffPrices on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into tariffPriceList
                               from tariffPrice in tariffPriceList.DefaultIfEmpty()
                               join productPriceNullable in productPrices on invoiceItem.ProductPriceId equals productPriceNullable.Id
                               into productPriceList
                               from productPrice in productPriceList.DefaultIfEmpty()
                               let productPriceValue = productPrice != null ? productPrice.Price : 0
                               let tariffPriceValue = tariffPrice != null ? tariffPrice.Price : 0

                               where transaction.Successful
                               where invoiceItem.CreateDateTime > startOfCurrentMonth

                               select new
                               {
                                   invoice,
                                   invoiceItem,
                                   tariffPrice = tariffPriceValue,
                                   productPrice = productPriceValue
                               }).GroupBy(x => x.invoice).ToListAsync();

            var invoiceResult = model.Select(x => new
            {
                invoice = x.Key,
                sum = x.Sum(y => y.tariffPrice + y.productPrice)
            }).ToList();

            var resultList = new List<InsightDto>();
            foreach (var item in invoiceResult)
            {
                var monthInsightItem = new InsightDto()
                {
                    CreateDateTime = item.invoice.CreateDateTime,
                    Value = item.sum,
                    DateTime = item.invoice.CreateDateTime,
                    Id = item.invoice.CreateDateTime.Day
                };

                if (resultList.Any(x => x.Id == monthInsightItem.Id))
                {
                    var instance = resultList.FirstOrDefault(x => x.Id == monthInsightItem.Id);
                    if (instance != null) instance.Value += monthInsightItem.Value;
                }
                else
                {
                    resultList.Add(monthInsightItem);
                }
            }

            return resultList;
        }
    }
}
