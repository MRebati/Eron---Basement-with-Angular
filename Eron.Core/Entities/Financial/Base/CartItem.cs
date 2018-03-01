using System;
using System.ComponentModel.DataAnnotations;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.Entities.Financial.Order;

namespace Eron.Core.Entities.Financial.Base
{
    public class CartItem : Entity<long>
    {
        public long ProductId { get; set; }

        [Required]
        public string UserId { get; set; }

        public bool IsSold { get; set; }

        public int Count { get; set; }

        #region Navigation Properties

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        #endregion Navigation Properties

        #region NotMapped

        [NotMapped]
        public string ProductName { get; set; }

        [NotMapped]
        public long ProductPrice { get; set; }

        [NotMapped]
        public string ProductCode { get; set; }

        [NotMapped]
        public string ProductImage { get; set; }

        #endregion NotMapped
    }
}