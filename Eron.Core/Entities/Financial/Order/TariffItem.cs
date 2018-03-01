using System;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.AppEnums;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Order
{
    public class TariffItem: Entity<long>
    {
        public TariffItemType Type { get; set; }

        public string Name { get; set; }

        public long TariffId { get; set; }

        [ForeignKey("TariffId")]
        public Tariff Tariff { get; set; }
    }
}