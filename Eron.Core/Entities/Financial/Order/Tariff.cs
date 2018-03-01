using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Order
{
    public class Tariff: Entity<long>
    {
        [Required]
        public string TariffName { get; set; }

        [Required]
        public CustomerType CustomerType { get; set; }

        public long? DesignPrice { get; set; }

        public UnitType UnitType { get; set; }

        public Guid? ImageId { get; set; }

        [ForeignKey("ImageId")]
        public virtual EronFile Image { get; set; }

        public int TariffCategoryId { get; set; }

        [ForeignKey("TariffCategoryId")]
        public TariffCategory TariffCategory { get; set; }

        public ICollection<TariffItem> TariffItems { get; set; }

        public ICollection<TariffPrice> TariffPrices { get; set; }
    }
}
