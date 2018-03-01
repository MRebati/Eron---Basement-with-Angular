using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Eron.Business.Core.Services.Financial.Base.Invoice;
using Eron.Business.Core.Services.Financial.Base.Invoice.Dto;
using Eron.Presentation.WebApplication.WebApi.Infrastructure;

namespace Eron.Presentation.WebApplication.WebApi.Api.Financial.Shop
{
    [Authorize(Roles ="Admin")]
    public class ShopController: BaseApiController
    {
        private readonly IInvoiceAppService _invoiceAppService;

        public ShopController(IInvoiceAppService invoiceAppService)
        {
            _invoiceAppService = invoiceAppService;
        }

        public async Task<IHttpActionResult> Get()
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IHttpActionResult> GetAllAsPagedList(InvoiceListRequestDto input)
        {
            //var result = await _invoiceAppService.GetAsPagedList(input);
            //return Ok(result);

            return Ok();
        }


    }
}