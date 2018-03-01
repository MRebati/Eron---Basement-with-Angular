using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Eron.Business.Core.Services.Financial.Order.Order;
using Eron.Business.Core.Services.Financial.Order.Order.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Order
{
    [RoutePrefix("api/order")]
    public class OrderController : BaseApiController
    {
        private readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        #region Query

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Get(OrderListRequestDto input)
        {
            var result = await _orderAppService.GetAllUsersOrdersAsPagedList(input);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Get(Guid id)
        {
            var result = await _orderAppService.GetById(id);
            return Ok(result);
        }

        [Authorize]
        [Route("user/all")]
        public async Task<IHttpActionResult> GetUserOrders()
        {
            var result = await _orderAppService.GetUserOrders();
            return Ok(result);
        }

        [Authorize]
        [Route("user/approved")]
        public async Task<IHttpActionResult> GetUserApproved()
        {
            var result = await _orderAppService.GetAllUsersOrders();
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("user/byNumber/{orderNumber}")]
        public async Task<IHttpActionResult> GetByNumberAsOwner(string orderNumber)
        {
            var result = await _orderAppService.GetByNumber(orderNumber);

            if (User.Identity.GetUserId() == result.UserId)
                return Ok(result);
            return Unauthorized();
        }

        [HttpGet]
        [Route("byNumber/{orderNumber}")]
        public async Task<IHttpActionResult> GetByNumberAsAdmin(string orderNumber)
        {
            var result = await _orderAppService.GetByNumber(orderNumber);

            if (User.IsInRole("admin") || User.Identity.GetUserId() == result.UserId)
                return Ok(result);
            return Unauthorized();
        }

        [Authorize(Roles = "admin")]
        [Route("designpricepending")]
        public async Task<IHttpActionResult> GetDesignPricePending()
        {
            var result = await _orderAppService.GetAllUsersOrders();
            return Ok(result);
        }

        [HttpPost]
        [Route("getaspagedlist")]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> AsPagedList(OrderListRequestDto input)
        {
            var result = await _orderAppService.GetAllUsersOrdersAsPagedList(input);
            return Ok(result);
        }

        #endregion Query

        #region Command

        [Authorize]
        public async Task<IHttpActionResult> Post(OrderCreateOrUpdateDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderAppService.CreateOrder(input);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("assigndesignprice")]
        public async Task<IHttpActionResult> AssignOrderDesignPrice(OrderDesignPriceDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _orderAppService.AssignOrderDesignPrice(input);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Delete(List<string> idList)
        {
            bool result = await _orderAppService.CancelOrderByOrderNumberAsUser(idList);
            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [Route("changeStateOfOrderList")]
        public async Task<bool> ChangeStateOfOrderList(OrderChangeStatusDto input)
        {
            var userId = User.Identity.GetUserId();
            var result = await _orderAppService.ChangeStateOfOrderList(input, userId);
            return result;
        }

        #endregion Command
    }
}
