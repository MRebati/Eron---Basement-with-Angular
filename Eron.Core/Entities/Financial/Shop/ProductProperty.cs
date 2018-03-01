using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Attributes;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Shop
{
    public class ProductProperty : Entity<long>
    {
        [NotMapped]
        public string Name { get; set; }

        public string Value { get; set; }

        public int ProductPropertyNameId { get; set; }

        public ProductPropertyName ProductPropertyName { get; set; }

        public long ProductId { get; set; }

        public Product Product { get; set; }

        #region Methods

        public ProductProperty Create(string name, string value)
        {
            this.Name = name;
            this.Value = value;
            return this;
        }

        public ProductProperty Create(int nameId, string value)
        {
            this.ProductPropertyNameId = nameId;
            this.Value = value;
            return this;
        }

        #endregion
    }
}