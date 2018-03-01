using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Base.BannerSlider;
using Eron.Business.Core.Services.Base.BannerSlider.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Base
{
    [RoutePrefix("api/bannerslider")]
    public class BannerSliderController: BaseApiController
    {
        private IBannerSliderAppService _service;

        public BannerSliderController(IBannerSliderAppService service)
        {
            _service = service;
        }

        public async Task<IHttpActionResult> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [Route("{groupName}")]
        public async Task<IHttpActionResult> Get(string groupName)
        {
            var result = await _service.GetByGroupName(groupName);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Post(List<BannerSliderCreateOrUpdateDto> input)
        {
            var result = await _service.CreateByGroup(input);
            return Ok(result);
        }

        public async Task<IHttpActionResult> Put(List<BannerSliderCreateOrUpdateDto> input)
        {
            var result = await _service.UpdateByGroup(input);
            return Ok(result);
        }

        //[Route("{groupName}")]
        //public async Task<IHttpActionResult> Delete(string groupName)
        //{
        //    var result = await _service.DeleteGroup(groupName);
        //    return Ok(result);
        //}

        [HttpDelete, Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            return Ok(result);
        }
    }
}