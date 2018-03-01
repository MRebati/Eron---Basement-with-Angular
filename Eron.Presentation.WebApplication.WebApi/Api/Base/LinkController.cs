using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Base.Navigation;
using Eron.Business.Core.Services.Base.Navigation.Dto;
using Eron.Core.AppEnums;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Base
{
    [Authorize(Roles = "admin")]
    [RoutePrefix("api/link")]
    public class LinkController : BaseApiController
    {
        private ILinkAppService _service;

        public LinkController(ILinkAppService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        [Route("{type}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get(int type)
        {
            LinkPlacement placement = (LinkPlacement)type;
            var result = await _service.GetByPlacement(placement);

            return Ok(result);
        }

        [Route("tree/{type}")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> GetByTree(int type)
        {
            LinkPlacement placement = (LinkPlacement)type;
            var result = await _service.GetByPlacementAsTree(placement);

            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(LinkCreateOrUpdateDto input)
        {
            var result = await _service.Create(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Put(LinkCreateOrUpdateDto input)
        {
            var result = await _service.Update(input);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<bool> Delete(int id)
        {
            var result = await _service.Delete(id);
            return result;
        }

        [Route("reorder")]
        public async Task<IHttpActionResult> ReOrder(List<LinkReOrderDto> input)
        {
            var result = await _service.ReOrder(input);
            return Ok(result);
        }
    }
}