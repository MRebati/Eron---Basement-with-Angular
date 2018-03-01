using System.Data.Entity;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Shop
{
    public class ProductPropertyRepository : Repository<ProductProperty>, IProductPropertyRepository
    {
        public ProductPropertyRepository(DbContext context) : base(context)
        {
        }
    }
}