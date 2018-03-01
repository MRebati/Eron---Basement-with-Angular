using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Order;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Order
{
    public interface ITariffPriceRepository : IRepository<TariffPrice>
    {
        List<TariffPrice> GetAllWithTariffId(long id);
        TariffPrice GetValidByTariffId(long id);
        List<TariffPrice> GetAllValid();
        Task<List<TariffPrice>> GetAllValidAsync();
    }
}