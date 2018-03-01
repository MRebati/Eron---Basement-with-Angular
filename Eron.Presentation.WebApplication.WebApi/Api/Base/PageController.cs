using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Pages;
using Eron.Business.Core.Services.Base.Pages.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Base
{
    [RoutePrefix("api/page")]
    [Authorize(Roles = "admin")]
    public class PageController : BaseApiController
    {
        private IPageAppService _service;
        public PageController(IPageAppService service)
        {
            _service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [AllowAnonymous]
        [Route("slug/{slug}")]
        public async Task<IHttpActionResult> GetBySlug(string slug)
        {
            var result = await _service.GetBySlug(slug);
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            var result = await _service.GetDetailsAsync(id);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(PageCreateUpdateDto input)
        {
            var result = await _service.Create(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Put(PageCreateUpdateDto input)
        {
            var result = await _service.Update(input);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var entityDto = new EntityDto<int>(id);
            var result = await _service.Delete(entityDto);
            return Ok(result);
        }
    }
}