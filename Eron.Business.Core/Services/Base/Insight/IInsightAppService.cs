using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Insight.Dto;

namespace Eron.Business.Core.Services.Base.Insight
{
    public interface IInsightAppService : IApplicationService
    {
        Task<double> GetLastMonthSales();
        Task<double> GetCurrentMonthSales();
        Task<double> GetCurrentDaySales();
        Task<double> GetClients();
        Task<double> GetCustomers();
        Task<double> GetPagesCount();
        Task<double> GetOrdersCount();
        Task<double> GetProductsCounts();
        Task<double> GetTariffCounts();
        Task<double> GetLastWeekSales();
        Task<double> GetCurrentWeekSales();
        Task<List<InsightDto>> GetLastMonthSalesWithDayByDayDetails();
        Task<List<InsightDto>> GetCurrentMonthSalesWithDayByDayDetails();
    }
}