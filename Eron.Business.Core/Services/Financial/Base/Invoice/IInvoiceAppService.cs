using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Base.Invoice.Dto;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Financial.Base.Invoice
{
    public interface IInvoiceAppService : IApplicationService
    {
        #region Query

        Task<List<InvoiceDto>> GetUserInvoices(string userId);

        Task<InvoiceDto> GetUserInvoice(string invoiceNumber);

        Task<InvoiceDto> GetInvoiceByNumber(string invoiceNumber);

        Task<List<InvoiceDto>> GetAllInvoices();

        Task<PagedListResult<InvoiceDto>> GetAllProductInvoicesAsPagedList(InvoiceListRequestDto input);

        Task<PagedListResult<InvoiceDto>> GetAllInvoicesAsPagedList(InvoiceListRequestDto input);

        #endregion Query

        #region Command

        Task<InvoiceDto> CreateInvoice(InvoiceCreateOrUpdateDto input);

        Task<InvoiceDto> CreateInvoiceByOrders(List<string> orderNumbers);

        Task<InvoiceDto> CreateInvoiceByCart();

        Task<List<InvoiceDto>> ChangeStateOfInvoiceList(InvoiceChangeStatusDto input, string userId);

        #endregion Command
    }
}