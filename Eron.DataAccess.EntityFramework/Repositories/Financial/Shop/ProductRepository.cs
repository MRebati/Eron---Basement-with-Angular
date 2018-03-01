using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Shop
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly IRepository<ProductProperty> _productPropertyRepository;
        private readonly IRepository<ProductPrice> _productPriceRepository;
        private readonly IRepository<InvoiceItem> _invoiceItemRepository;
        private readonly IRepository<Invoice> _invoiceRepository;
        private readonly IRepository<ProductCategory> _productCategoryRepository;

        public ProductRepository(
            DbContext context,
            IRepository<ProductProperty> productPropertyRepository,
            IRepository<ProductPrice> productPriceRepository,
            IRepository<InvoiceItem> invoiceItemRepository,
            IRepository<Invoice> invoiceRepository,
            IRepository<ProductCategory> productCategoryRepository) : base(context)
        {
            _productPropertyRepository = productPropertyRepository;
            _productPriceRepository = productPriceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceRepository = invoiceRepository;
            _productCategoryRepository = productCategoryRepository;
        }

        public override Product GetById(object id)
        {
            var result = (
                from product in base.GetQueryable()
                join property in _productPropertyRepository.GetQueryable() on product.Id equals property.ProductId
                join category in _productCategoryRepository.GetQueryable() on product.CategoryId equals category.Id
                where product.Id == (long)id
                //where product.ExistsInShop
                select new
                {
                    product,
                    category
                }
                ).FirstOrDefault();

            var entity = result.product;
            entity.CategoryName = result.category.Title;

            return entity;
        }

        public override async Task<Product> GetByIdAsync(object id)
        {
            var result = await (
                from product in base.GetQueryable()
                join property in _productPropertyRepository.GetQueryable() on product.Id equals property.ProductId
                join category in _productCategoryRepository.GetQueryable() on product.CategoryId equals category.Id
                where product.Id == (long)id
                where product.IsDeleted == false
                //where product.ExistsInShop
                select new
                {
                    product,
                    category
                }
            ).FirstOrDefaultAsync();

            var entity = result.product;
            entity.CategoryName = result.category.Title;

            return entity;
        }

        public async Task<List<Product>> GetBestSellerProducts()
        {
            var lastThirtyDays = DateTime.Now.AddMonths(-1);
            var result = await (from product in base.GetQueryable()
                                join productPrice in _productPriceRepository.GetQueryable() on product.Id equals productPrice.ProductId
                                join invoiceItem in _invoiceItemRepository.GetQueryable() on productPrice.Id equals invoiceItem.ProductPriceId
                                join invoice in _invoiceRepository.GetQueryable() on invoiceItem.InvoiceId equals invoice.Id
                                where productPrice.IsValid
                                where product.ExistsInShop
                                where product.IsDeleted == false
                                where invoiceItem.CreateDateTime > lastThirtyDays
                                select product).Distinct().ToListAsync();

            return result;
        }

        public async Task<List<Product>> GetPromotedProducts()
        {
            var result = await (from product in GetQueryable()
                                join productCategory in _productCategoryRepository.GetQueryable() on product.CategoryId equals
                                productCategory.Id
                                where productCategory.IsDeleted == false
                                where productCategory.Promoted
                                where product.IsDeleted == false

                                select product
                                ).ToListAsync();
            return result;
        }
    }
}