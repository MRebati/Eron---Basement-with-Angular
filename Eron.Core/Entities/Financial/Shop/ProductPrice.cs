using System;
using System.Collections.Generic;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Shop
{
    public class ProductPrice : Entity<Guid>
    {
        public ProductPrice() { }

        public ProductPrice(long entityId, long productPrice)
        {
            this.ProductId = entityId;
            this.Price = productPrice;
            this.Id = Guid.NewGuid();
            this.IsValid = true;
        }

        public bool IsValid { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        public long Price { get; set; }

        public string Description { get; set; }

        public ICollection<Offer> Offers { get; set; }
    }
}