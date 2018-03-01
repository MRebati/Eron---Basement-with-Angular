using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Shop
{
    public class ProductPriceRepository : Repository<ProductPrice>, IProductPriceRepository
    {
        public ProductPriceRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<ProductPrice>> GetActiveForProducts(IEnumerable<long> idList)
        {
            var result = GetQueryable().Where(x => x.IsValid && idList.Contains(x.ProductId));
            return await result.ToListAsync();
        }

        public Task<ProductPrice> GetActiveForProduct(long productId)
        {
            return GetOneAsync(x => x.ProductId == productId && x.IsValid);
        }
    }
}