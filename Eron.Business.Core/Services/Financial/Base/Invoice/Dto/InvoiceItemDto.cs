using System;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Base.Cart.Dto;
using Eron.Business.Core.Services.Financial.Order.TariffPrice.Dto;
using Eron.Business.Core.Services.Financial.Shop.ProductPrice.Dto;

namespace Eron.Business.Core.Services.Financial.Base.Invoice.Dto
{
    public class InvoiceItemDto : EntityDto<long>
    {
        public int Count { get; set; }

        public string Description { get; set; }

        public Guid? OrderId { get; set; }

        public long? CartItemId { get; set; }

        public Guid? ProductPriceId { get; set; }

        public Guid? TariffPriceId { get; set; }

        public long InvoiceId { get; set; }

        public virtual TariffPriceDto TariffPrice { get; set; }

        public virtual ProductPriceDto ProductPrice { get; set; }

        public virtual CartItemDto CartItem { get; set; }
    }
}