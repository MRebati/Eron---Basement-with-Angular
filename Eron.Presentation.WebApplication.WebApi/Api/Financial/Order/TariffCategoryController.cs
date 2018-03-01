using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Financial.Order.TariffCategory;
using Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Order
{
    [RoutePrefix("api/tariffcategories")]
    public class TariffCategoriesController : BaseApiController
    {
        private ITariffCategoryAppService _service;

        public TariffCategoriesController(ITariffCategoryAppService service)
        {
            _service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _service.GetById(id);
            return Ok(result);
        }

        [Route("flat")]
        public async Task<IHttpActionResult> GetFlat()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [Route("full")]
        public async Task<IHttpActionResult> GetFull()
        {
            var result = await _service.GetFullCategories();
            return Ok(result);
        }

        [Route("promoted")]
        public async Task<IHttpActionResult> GetPromotedCategories()
        {
            var result = await _service.GetPromoted();
            return Ok(result);
        }

        [Route("homePage")]
        public async Task<IHttpActionResult> GetHomePageCategories()
        {
            var result = await _service.GetHomePage();
            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(TariffCategoryCreateOrUpdateDto input)
        {
            var result = await _service.Create(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Put(TariffCategoryCreateOrUpdateDto input)
        {
            var result = await _service.Update(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return Ok(result);
        }

        [Route("reorder")]
        public async Task<IHttpActionResult> ReOrder(List<TariffCategoryReOrderDto> input)
        {
            var result = await _service.ReOrder(input);
            return Ok(result);
        }
    }
}