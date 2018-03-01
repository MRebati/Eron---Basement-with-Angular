using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Shop
{
    public class ProductCategory: Entity<int>
    {
        public string Title { get; set; }

        public string Slug { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public bool Promoted { get; set; }

        public bool ViewOnHomePage { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual ProductCategory Parent { get; set; }

        public virtual ICollection<ProductCategory> Children { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
