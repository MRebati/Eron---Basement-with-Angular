using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Shop
{
    public interface IProductPriceRepository : IRepository<ProductPrice>
    {
        Task<List<ProductPrice>> GetActiveForProducts(IEnumerable<long> idList);

        Task<ProductPrice> GetActiveForProduct(long productId);
    }
}