using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Shop
{
    [RoutePrefix("api/productcategories")]
    public class ProductCategoriesController: BaseApiController
    {
        private IProductCategoryAppService _service;

        public ProductCategoriesController(IProductCategoryAppService service)
        {
            _service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.GetTree();
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

        public async Task<IHttpActionResult> Post(ProductCategoryCreateOrUpdateDto input)
        {
            var result = await _service.Create(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Put(ProductCategoryCreateOrUpdateDto input)
        {
            var result = await _service.Update(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }

        [Route("reorder")]
        public async Task<IHttpActionResult> ReOrder(List<ProductCategoryReOrderDto> input)
        {
            var result = await _service.ReOrder(input);
            return Ok(result);
        }
    }
}