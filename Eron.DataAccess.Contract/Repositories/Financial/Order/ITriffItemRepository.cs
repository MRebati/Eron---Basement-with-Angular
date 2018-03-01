using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Order;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Order
{
    public interface ITariffItemRepository : IRepository<TariffItem>
    {
        List<TariffItem> GetAllWithTariffId(long id);

        Task<List<TariffItem>> GetAllWithTariffIdAsync(long id);

        Task<List<TariffItem>> GetAllWithTariffIdsAsync(List<long> idList);
        
    }
}