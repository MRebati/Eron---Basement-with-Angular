using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Order
{
    public class TariffPrice : Entity<Guid>
    {
        public long Price { get; set; }

        public bool IsValid { get; set; }

        public long TariffId { get; set; }

        [ForeignKey("TariffId")]
        public Tariff Tariff { get; set; }

        public TariffPrice Create(long price, long tariffId)
        {
            this.Id = Guid.NewGuid();
            this.IsValid = true;
            this.TariffId = tariffId;
            this.Price = price;
            return this;
        }
    }
}