using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Shop
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetBestSellerProducts();
        Task<List<Product>> GetPromotedProducts();
    }
}