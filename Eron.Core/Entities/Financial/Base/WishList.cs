using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class WishListItem : Entity<long>
    {
        public long ProductId { get; set; }

        public string UserId { get; set; }

        public bool IsSold { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}