using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Financial.Shop.Product;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;
using Eron.Core.Entities.Financial.Base;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Shop
{
    [Authorize]
    [RoutePrefix("api/products")]
    public class ProductsController: BaseApiController
    {
        private IProductAppService _service;

        public ProductsController(IProductAppService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("category/{categoryId}")]
        public async Task<IHttpActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _service.GetByCategoryId(categoryId);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("productcode/{productCode}")]
        public async Task<IHttpActionResult> GetByProductCode(string productCode)
        {
            var result = await _service.GetByProductCode(productCode);
            return Ok(result);
        }

        [Route("productcode/update/{productCode}")]
        public async Task<IHttpActionResult> GetByProductCodeForUpdate(string productCode)
        {
            var result = await _service.GetByProductCodeForUpdate(productCode);
            return Ok(result);
        }

        [AllowAnonymous]
        public IHttpActionResult Get(long id)
        {
            ProductDto result = _service.GetById(id);
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("bestseller")]
        public async Task<IHttpActionResult> GetBestSeller()
        {
            List<ProductDto> result = await _service.GetBestSellers();
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("promotion")]
        public async Task<IHttpActionResult> GetPromoted()
        {
            List<ProductDto> result = await _service.GetPromoted();
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("related/{productCode}")]
        public async Task<IHttpActionResult> GetRelatedProducts(string productCode)
        {
            List<ProductDto> result = await _service.GetRelatedProductsByProductCode(productCode);
            return Ok(result);
        }

        [HttpPost]
        [Route("aspagedlist")]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> GetAllAsPagedList(ProductListRequestDto input)
        {
            var result = await _service.GetAllAsPagedList(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(ProductCreateOrUpdateDto input)
        {
            var result = await _service.Create(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Put(ProductCreateOrUpdateDto input)
        {
            var result = await _service.Update(input);
            return Ok(result);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}