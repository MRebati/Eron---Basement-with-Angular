using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Base.Search.Dto;
using Eron.Business.Core.Services.Base.Search.SearchInControlPanel;
using Eron.Business.Core.Services.Base.Search.SearchInWebsite;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Base
{
    public class SearchController : BaseApiController
    {
        private readonly ISearchInWebsiteAppService _searchInWebsiteAppService;
        private readonly ISearchInControlPanelAppService _searchInControlPanelAppService;


        public SearchController(
            ISearchInWebsiteAppService searchInWebsiteAppService,
            ISearchInControlPanelAppService searchInControlPanelAppService)
        {
            _searchInWebsiteAppService = searchInWebsiteAppService;
            _searchInControlPanelAppService = searchInControlPanelAppService;
        }

        public async Task<IHttpActionResult> Get(string id)
        {
            var result =  await _searchInWebsiteAppService.Search(id);

            return Ok(result);
        }
    }
}