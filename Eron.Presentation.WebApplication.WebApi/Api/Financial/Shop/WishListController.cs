using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.WishList;
using Eron.Business.Core.Services.Financial.Shop.WishList.Dto;
using Eron.Core.Entities.Financial.Base;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Microsoft.AspNet.Identity;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Shop
{
    [Authorize]
    [System.Web.Mvc.RoutePrefix("api/wishlist")]
    public class WishListController : BaseApiController
    {
        private readonly IWishListAppService _service;

        public WishListController(IWishListAppService service)
        {
            _service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var userId = User.Identity.GetUserId();
            var result = await _service.GetUserList(userId);

            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(WishListItemCreateDto input)
        {
            input.UserId = User.Identity.GetUserId();
            var exists = await _service.ProductExistsInUserWishList(input.ProductId, input.UserId);
            if (!exists)
            {
                var result = await _service.Create(input);
                return Ok(result);
            }
            else
            {
                return Conflict();
            }
            
        }

        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}