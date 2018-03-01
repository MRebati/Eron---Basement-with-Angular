using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Financial.Base.Cart;
using Eron.Business.Core.Services.Financial.Base.Cart.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Base
{
    [Authorize]
    public class CartController: BaseApiController
    {
        private ICartAppService _service;

        public CartController(ICartAppService service)
        {
            _service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var entityList = await _service.GetUserCartList();
            return Ok(entityList);
        }

        public async Task<IHttpActionResult> Get(long id)
        {
            var entityList = await _service.GetUserCartList();
            if (entityList.Any(x => x.Id == id))
            {
                var result = entityList.FirstOrDefault(x => x.Id == id);
                return Ok(result);
            }
            return NotFound();
        }

        public async Task<IHttpActionResult> Post(CartItemCreateOrUpdateDto input)
        {
            var result = await _service.Create(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Put(CartItemCreateOrUpdateDto input)
        {
            var result = await _service.Update(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}