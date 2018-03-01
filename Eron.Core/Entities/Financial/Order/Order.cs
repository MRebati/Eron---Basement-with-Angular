using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.Core.Entities.User;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Order
{
    public class Order: Entity<Guid>
    {
        [MaxLength(25)]
        [Index(IsUnique = true)]
        public string OrderNumber { get; set; }

        public bool Approved { get; set; }

        public int Count { get; set; }

        public string Description { get; set; }

        public bool HasDesignOrder { get; set; }

        public long DesignPrice { get; set; }

        public string UserId { get; set; }

        public long TariffId { get; set; }

        [ForeignKey("TariffId")]
        public Tariff Tariff { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }

        public ICollection<EronFile> Images { get; set; }

        public OrderStatusType OrderStatus { get; set; }

        #region NotMapped

        [NotMapped]
        public long Price { get; set; }

        [NotMapped]
        public long Quantity { get; set; }

        [NotMapped]
        public string TariffName { get; set; }

        [NotMapped]
        public long FinalPrice { get; set; }

        [NotMapped]
        public long? InvoiceId { get; set; }

        [NotMapped]
        public string InvoiceNumber { get; set; }

        #endregion NotMapped
    }
}