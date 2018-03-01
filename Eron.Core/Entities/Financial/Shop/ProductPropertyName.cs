using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Shop
{
    public class ProductPropertyName : Entity<int>
    {
        public ProductPropertyName()
        {
        }

        public ProductPropertyName(string name)
        {
            this.Name = name;
        }

        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Name { get; set; }
    }
}