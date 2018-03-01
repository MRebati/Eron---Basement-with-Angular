using System.Data.Entity;
using Eron.Core.Entities.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Shop
{
    public class InvoiceLogRepository : Repository<InvoiceLog>, IInvoiceLogRepository
    {
        public InvoiceLogRepository(DbContext context) : base(context)
        {
        }
    }
}