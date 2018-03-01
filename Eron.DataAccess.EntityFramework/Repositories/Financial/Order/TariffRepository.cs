using System.Data.Entity;
using Eron.Core.Entities.Financial.Order;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Order;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Order
{
    public class TariffRepository : Repository<Tariff>, ITariffRepository
    {
        public TariffRepository(DbContext context) : base(context)
        {
        }
    }
}