using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using AutoMapper.Attributes;
using Eron.Core.Entities.Base;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Shop
{
    public class Product : Entity<long>
    {
        public string Name { get; set; }

        public string ProductCode { get; set; }

        public bool ExistsInShop { get; set; }

        public Guid DefaultImage { get; set; }

        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [DataType(DataType.Html)]
        public string LongDescription { get; set; }

        #region Navigation

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory Category { get; set; }

        public virtual ICollection<EronFile> ProductImages { get; set; }

        public virtual ICollection<ProductProperty> Properties { get; set; }

        public virtual ICollection<ProductPrice> Prices { get; set; }

        #endregion Navigation

        #region NotMapped

        [NotMapped]
        public string CategoryName { get; set; }

        [NotMapped]
        public List<string> ParentCategories { get; set; }

        [NotMapped]
        public long Price { get; set; }

        #endregion NotMapped
    }
}