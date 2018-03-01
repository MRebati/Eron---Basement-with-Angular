using System.Data.Entity;
using Eron.Core.Entities.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Shop
{
    public class FinanceTransactionRepository : Repository<FinanceTransaction>, IFinanceTransactionRepository
    {
        public FinanceTransactionRepository(DbContext context) : base(context)
        {
        }
    }
}