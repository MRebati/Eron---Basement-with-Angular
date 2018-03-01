using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Shop;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Shop
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        private readonly IRepository<InvoiceItem> _invoiceItemRepository;
        private readonly IRepository<ProductPrice> _productPriceRepository;
        private readonly IRepository<TariffPrice> _tariffPriceRepository;

        public InvoiceRepository(
            DbContext context,
            IRepository<InvoiceItem> invoiceItemRepository,
            IRepository<ProductPrice> productPriceRepository,
            IRepository<TariffPrice> tariffPriceRepository) : base(context)
        {
            _invoiceItemRepository = invoiceItemRepository;
            _productPriceRepository = productPriceRepository;
            _tariffPriceRepository = tariffPriceRepository;
        }

        public async Task<List<Invoice>> GetByNumberListAsync(List<string> invoiceNumberList)
        {
            var result = await (from invoice in GetQueryable(x => invoiceNumberList.Select(y => y.ToLower()).Contains(x.InvoiceNumber))
                                select invoice).ToListAsync();
            return result;
        }

        public async Task<long> GetInvoiceAmount(long invoiceId)
        {
            long finalPrice = 0;
            var result = await (from invoiceItem in _invoiceItemRepository.GetQueryable()
                         join productPriceNullable in _productPriceRepository.GetQueryable() on invoiceItem.ProductPriceId equals productPriceNullable.Id into productPriceList
                         from productPrice in productPriceList.DefaultIfEmpty()
                         join tariffPriceNullable in _tariffPriceRepository.GetQueryable() on invoiceItem.TariffPriceId equals tariffPriceNullable.Id into tariffPriceList
                         from tariffPrice in tariffPriceList.DefaultIfEmpty()
                         where invoiceItem.InvoiceId == invoiceId
                         select new
                         {
                             tariffPrice,
                             productPrice
                         }).ToListAsync();

            foreach (var item in result)
            {
                if (item.productPrice != null)
                    finalPrice += item.productPrice.Price;
                if (item.tariffPrice != null)
                    finalPrice += item.tariffPrice.Price;
            }

            return finalPrice;
        }
    }
}
