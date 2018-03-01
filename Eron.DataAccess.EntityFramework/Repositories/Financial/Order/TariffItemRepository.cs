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
    public class TariffItemRepository : Repository<TariffItem>, ITariffItemRepository
    {
        public TariffItemRepository(DbContext context) : base(context)
        {
        }

        public List<TariffItem> GetAllWithTariffId(long id)
        {
            return this.GetQueryable().Where(x => x.TariffId == id).ToList();
        }

        public Task<List<TariffItem>> GetAllWithTariffIdAsync(long id)
        {
            return this.GetQueryable().Where(x => x.TariffId == id).ToListAsync();
        }

        public async Task<List<TariffItem>> GetAllWithTariffIdsAsync(List<long> idList)
        {
            var entityList = await this.GetQueryable().Where(x => idList.Contains(x.TariffId)).ToListAsync();
            return entityList;
        }
    }
}