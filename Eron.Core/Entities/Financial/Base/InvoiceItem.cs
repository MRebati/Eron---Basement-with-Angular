using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class InvoiceItem : Entity<long>
    {
        public int Count { get; set; }

        public string Description { get; set; }

        public Guid? OrderId { get; set; }

        public long? CartItemId { get; set; }

        public Guid? ProductPriceId { get; set; }

        public Guid? TariffPriceId { get; set; }

        public long InvoiceId { get; set; }

        #region Navigation

        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        [ForeignKey("ProductPriceId")]
        public virtual ProductPrice ProductPrice { get; set; }

        [ForeignKey("TariffPriceId")]
        public virtual TariffPrice TariffPrice { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order.Order Order { get; set; }

        [ForeignKey("CartItemId")]
        public virtual CartItem CartItem { get; set; }
        #endregion

    }
}