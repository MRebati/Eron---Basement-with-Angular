using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Infrastructure;
using Eron.Core.ValueObjects;

namespace Eron.Presentation.WebApplication.WebApi.Infrastructure
{
    public class CommonAsyncCrudWebApiController<TPrimaryKey, TEntity, TEntityDto, TEntityCreateOrUpdateDto, TEntityListDto> : BaseApiController
        where TEntityDto : EntityDto<TPrimaryKey>
        where TEntityCreateOrUpdateDto : IEntityEntryDto
        where TEntityListDto : IPagedListRequest<TEntity>
    {
        protected readonly IAsyncCrudAppService<TPrimaryKey, TEntity, TEntityDto, TEntityCreateOrUpdateDto, TEntityListDto> Service;

        public CommonAsyncCrudWebApiController(IAsyncCrudAppService<TPrimaryKey, TEntity, TEntityDto, TEntityCreateOrUpdateDto, TEntityListDto> service)
        {
            Service = service;
        }

        public virtual async Task<IHttpActionResult> Get()
        {
            var result = await Service.GetAll();
            return Ok(result);
        }

        public virtual async Task<IHttpActionResult> Get(TPrimaryKey id)
        {
            var result = await Service.GetById(id);
            return Ok(result);
        }

        [Route("")]
        public virtual async Task<IHttpActionResult> Post(TEntityCreateOrUpdateDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await Service.Create(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                HttpError error;
#if DEBUG
                error = new HttpError(ex, true);
#else
                error = new HttpError(ex, false);
#endif
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, error));
            }
        }

        [HttpPost]
        [Route("asPagedList")]
        public virtual async Task<IHttpActionResult> GetAsPagedList(TEntityListDto input)
        {
            var result = await Service.GetAllAsPagedList(input);
            return Ok(result);
        }



        public virtual async Task<IHttpActionResult> Put(TEntityCreateOrUpdateDto input)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await Service.Update(input);
                return Ok(result);
            }
            catch (Exception ex)
            {
                HttpError error;
#if DEBUG
                error = new HttpError(ex, true);
#else
                error = new HttpError(ex, false);
#endif
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.BadRequest, error));
            }
        }

        public virtual async Task<IHttpActionResult> Delete(TPrimaryKey id)
        {
            var result = await Service.Delete(id);
            return Ok(result);
        }

        [Route("logically/{id}")]
        public virtual async Task<IHttpActionResult> DeleteLogically(TPrimaryKey id)
        {
            var result = await Service.DeleteLogically(id);
            return Ok(result);
        }
    }
}