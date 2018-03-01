using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Base.Insight;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Base
{
    [RoutePrefix("api/insight")]
    public class InsightController: BaseApiController
    {
        private readonly IInsightAppService _insightAppService;

        public InsightController(IInsightAppService insightAppService)
        {
            _insightAppService = insightAppService;
        }

        [Authorize(Roles = "admin")]
        [Route("GetLastMonthSales")]
        public async Task<IHttpActionResult> GetLastMonthSales()
        {
            var result = await _insightAppService.GetLastMonthSales();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetCurrentMonthSales")]
        public async Task<IHttpActionResult> GetCurrentMonthSales()
        {
            var result = await _insightAppService.GetCurrentMonthSales();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetCurrentDaySales")]
        public async Task<IHttpActionResult> GetCurrentDaySales()
        {
            var result = await _insightAppService.GetCurrentDaySales();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetClients")]
        public async Task<IHttpActionResult> GetClients()
        {
            var result = await _insightAppService.GetClients();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetCustomers")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            var result = await _insightAppService.GetCustomers();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetPagesCount")]
        public async Task<IHttpActionResult> GetPagesCount()
        {
            var result = await _insightAppService.GetPagesCount();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetOrdersCount")]
        public async Task<IHttpActionResult> GetOrdersCount()
        {
            var result = await _insightAppService.GetOrdersCount();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetProductsCounts")]
        public async Task<IHttpActionResult> GetProductsCounts()
        {
            var result = await _insightAppService.GetProductsCounts();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetTariffCounts")]
        public async Task<IHttpActionResult> GetTariffCounts()
        {
            var result = await _insightAppService.GetTariffCounts();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetLastWeekSales")]
        public async Task<IHttpActionResult> GetLastWeekSales()
        {
            var result = await _insightAppService.GetLastWeekSales();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetCurrentWeekSales")]
        public async Task<IHttpActionResult> GetCurrentWeekSales()
        {
            var result = await _insightAppService.GetCurrentDaySales();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetLastMonthSalesWithDayByDayDetails")]
        public async Task<IHttpActionResult> GetLastMonthSalesWithDayByDayDetails()
        {
            var result = await _insightAppService.GetLastMonthSalesWithDayByDayDetails();
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("GetCurrentMonthSalesWithDayByDayDetails")]
        public async Task<IHttpActionResult> GetCurrentMonthSalesWithDayByDayDetails()
        {
            var result = await _insightAppService.GetCurrentMonthSalesWithDayByDayDetails();
            return Ok(result);
        }
    }
}