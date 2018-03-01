using System.Collections.Generic;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class Offer : Entity<long>
    {
        public string Code { get; set; }

        public long MaximumUsage { get; set; }

        public long RemainingUsable { get; set; }

        public int? Percent { get; set; }

        public long? Amount { get; set; }

        public bool IsValid { get; set; }

        public ICollection<ProductPrice> ProductPrices { get; set; }

        public ICollection<TariffPrice> TariffPrices { get; set; }
    }
}