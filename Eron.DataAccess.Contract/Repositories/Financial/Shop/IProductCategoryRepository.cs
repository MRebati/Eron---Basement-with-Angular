using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Shop
{
    public interface IProductCategoryRepository: IRepository<ProductCategory>
    {
        Task<List<ProductCategory>> GetFullCategories();

        Task<List<ProductCategory>> GetHomePageCategories();
        
    }
}
