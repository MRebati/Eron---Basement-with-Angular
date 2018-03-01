using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Eron.Business.Core.Services.Common;
using Eron.Business.Core.Services.Common.Dto;
using Eron.Business.Core.Services.Financial.Order.Tariff;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;
using Eron.SharedKernel.Helpers.FileHelper;

namespace Eron.Presentation.WebApplication.WebApi.Api.Common
{
    public class SelectListController : BaseApiController
    {
        private ICommonAppService _commonService;

        public SelectListController(
            ICommonAppService commonService)
        {
            _commonService = commonService;
        }

        // GET api/<controller>
        [Route("api/selectlist/get/{id}")]
        public List<SelectListObjectDto> Get(string id)
        {
            var result = _commonService.GetEnumAsSelectList(id);
            return result;
        }

        public List<SelectListObjectDto> GetEnglishTitles(string id)
        {
            var result = _commonService.GetEnumAsSelectListEnglishTitles(id);
            return result;
        }
    }
}