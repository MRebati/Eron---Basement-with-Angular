using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Base
{
    public class CartRepository : Repository<CartItem>, ICartRepository
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductPrice> _productPriceRepository;
        private readonly IRepository<InvoiceItem> _invoiceItemRepository;
        private readonly IRepository<Invoice> _invoiceRepository;


        public CartRepository(
            DbContext context,
            IRepository<Product> productRepository,
            IRepository<ProductPrice> productPriceRepository,
            IRepository<InvoiceItem> invoiceItemRepository,
            IRepository<Invoice> invoiceRepository) : base(context)
        {
            _productRepository = productRepository;
            _productPriceRepository = productPriceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<List<CartItem>> GetUserCartList(string userName)
        {
            var cartItemsWithInvoice = await (from invoiceItem in _invoiceItemRepository.GetQueryable(x => x.CartItemId.HasValue)
                                              join invoice in _invoiceRepository.GetQueryable() on invoiceItem.InvoiceId equals invoice.Id
                                              where invoice.UserId == userName
                                              select invoiceItem.CartItemId.Value).ToListAsync();

            var result = await (from cartItem in this.GetQueryable()
                                join product in _productRepository.GetQueryable() on cartItem.ProductId equals product.Id
                                join productPrice in _productPriceRepository.GetQueryable() on product.Id equals productPrice.ProductId
                                where productPrice.IsValid
                                where cartItem.UserId == userName
                                where !cartItemsWithInvoice.Contains(cartItem.Id)
                                select new
                                {
                                    cartItem,
                                    productName = product.Name,
                                    productPrice = productPrice.Price,
                                    productCode = product.ProductCode,
                                    productImage = product.DefaultImage
                                }).ToListAsync();

            foreach (var item in result)
            {
                item.cartItem.ProductName = item.productName;
                item.cartItem.ProductPrice = item.productPrice;
                item.cartItem.ProductCode = item.productCode;
                item.cartItem.ProductImage = item.productImage.ToString();
            }

            return result.Select(x => x.cartItem).ToList();
        }

        public async Task<List<CartItem>> GetUserUnpaidCartList(string user)
        {
            var paidItems = await (from cartItem in this.GetQueryable()
                                   join invoiceItem in _invoiceItemRepository.GetQueryable() on cartItem.Id equals invoiceItem.CartItemId
                                   select cartItem.Id).ToListAsync();

            var result = await (from cartItem in this.GetQueryable()
                                join product in _productRepository.GetQueryable() on cartItem.ProductId equals product.Id
                                join productPrice in _productPriceRepository.GetQueryable() on product.Id equals productPrice.ProductId
                                where productPrice.IsValid
                                where !paidItems.Contains(cartItem.Id)
                                where cartItem.UserId == user
                                select new
                                {
                                    cartItem,
                                    productName = product.Name,
                                    productPrice = productPrice.Price,
                                    productCode = product.ProductCode,
                                    productImage = product.DefaultImage
                                }).ToListAsync();

            foreach (var item in result)
            {
                item.cartItem.ProductName = item.productName;
                item.cartItem.ProductPrice = item.productPrice;
                item.cartItem.ProductCode = item.productCode;
                item.cartItem.ProductImage = item.productImage.ToString();
            }

            return result.Select(x => x.cartItem).ToList();
        }
    }
}