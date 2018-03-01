using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Eron.Business.Core.Services.Financial.Order.Tariff;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Eron.SharedKernel.Helpers.FileHelper;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Order
{
    [RoutePrefix("api/tariff")]
    public class TariffController : BaseApiController
    {
        private ITariffAppService _tariffAppService;
        public TariffController(
            ITariffAppService tariffAppService)
        {
            _tariffAppService = tariffAppService;
        }

        // GET api/<controller>

        public async Task<List<TariffDto>> Get()
        {
            var result = await _tariffAppService.GetAll();
            return result;
        }

        public async Task<List<TariffDto>> Get([FromUri]int? pageNumber, [FromUri]int? pageSize)
        {
            var tariffListRequest = new TariffListRequestDto().Create(pageSize, pageNumber,null,null);
            var result = await _tariffAppService.GetTariffList(tariffListRequest);
            return result.Result;
        }

        public async Task<List<TariffDto>> Get([FromUri]int? pageNumber, [FromUri]int? pageSize,string order, string orderBy)
        {
            var tariffListRequest = new TariffListRequestDto().Create(pageSize,pageNumber,order,orderBy);
            var result = await _tariffAppService.GetTariffList(tariffListRequest);
            return result.Result;
        }

        // GET api/<controller>/5
        public async Task<TariffDto> Get(long id)
        {
            var result = await _tariffAppService.GetAsync(id);
            return result;
        }

        [Route("category/{categoryId}")]
        public async Task<IHttpActionResult> GetByCategoryId(int categoryId)
        {
            var result = await _tariffAppService.GetByCategoryAsync(categoryId);
            return Ok(result);
        }

        // POST api/<controller>
        public async Task<TariffDto> Post(TariffCreateOrUpdateDto model)
        {
            var result = await _tariffAppService.Create(model);
            return result;
        }

        // PUT api/<controller>/5
        public async Task<TariffDto> Put(TariffCreateOrUpdateDto model)
        {
            var result = await _tariffAppService.Update(model);
            return result;
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(int id)
        {
            return await _tariffAppService.Delete(id);
        }
    }
}