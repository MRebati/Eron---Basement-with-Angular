using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Shop
{
    public class InvoiceItemRepository : Repository<InvoiceItem>, IInvoiceItemRepository
    {
        public InvoiceItemRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<InvoiceItem>> GetMonthSales()
        {
            var monthStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var result = await (GetQueryable(x => x.CreateDateTime > monthStart)).ToListAsync();
            return result;
        }
    }
}