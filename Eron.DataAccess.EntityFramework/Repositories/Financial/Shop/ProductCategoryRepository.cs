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
    public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductPrice> _productPriceRepository;
        public ProductCategoryRepository(
            DbContext context,
            IRepository<Product> productRepository,
            IRepository<ProductPrice> productPriceRepository) : base(context)
        {
            _productRepository = productRepository;
            _productPriceRepository = productPriceRepository;
        }

        public async Task<List<ProductCategory>> GetFullCategories()
        {
            var productCategories = (from category in GetQueryable()
                                     join product in _productRepository.GetQueryable() on category.Id equals product.CategoryId
                                     select category).Distinct();

            return await productCategories.ToListAsync();
        }

        public async Task<List<ProductCategory>> GetHomePageCategories()
        {
            var productCategories = await (from category in GetQueryable()
                                           join product in _productRepository.GetQueryable() on category.Id equals product.CategoryId
                                           join productPrice in _productPriceRepository.GetQueryable() on product.Id equals productPrice.ProductId
                                           where category.ViewOnHomePage
                                           where productPrice.IsValid

                                           select new
                                           {
                                               category,
                                               product,
                                               productPrice
                                           }).ToListAsync();
            foreach (var item in productCategories)
            {
                item.product.Price = item.productPrice.Price;
            }

            foreach (var item in productCategories.Select(x => x.category))
            {
                item.Products = productCategories
                                    .Select(x => x.product)
                                    .Where(x => x.CategoryId == item.Id)
                                    .ToList();
            }
            return productCategories.Select(x => x.category).ToList();
        }
    }
}