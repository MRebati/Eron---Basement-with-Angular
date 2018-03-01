using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Order;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Order;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Order
{
    public class TariffPriceRepository : Repository<TariffPrice>, ITariffPriceRepository
    {
        public TariffPriceRepository(DbContext context) : base(context)
        {
        }

        public List<TariffPrice> GetAllWithTariffId(long id)
        {
            return this.GetQueryable().Where(x => x.TariffId == id).ToList();
        }

        public TariffPrice GetValidByTariffId(long id)
        {
            return this.GetQueryable().FirstOrDefault(x => x.IsValid && x.TariffId == id);
        }

        public async Task<List<TariffPrice>> GetAllValidAsync()
        {
            return await this.GetQueryable(x => x.IsValid).ToListAsync();
        }

        public List<TariffPrice> GetAllValid()
        {
            return this.GetQueryable(x => x.IsValid).ToList();
        }
    }
}