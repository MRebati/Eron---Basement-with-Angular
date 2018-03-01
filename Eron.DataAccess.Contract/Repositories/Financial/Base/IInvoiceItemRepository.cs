using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Base
{
    public interface IInvoiceItemRepository : IRepository<InvoiceItem>
    {
        Task<List<InvoiceItem>> GetMonthSales();
    }
}