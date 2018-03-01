using System;
using System.Collections.Generic;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Financial.Shop.Product.Dto
{
    [MapsFrom(typeof(ProductPropertyDto), ReverseMap = true)]
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Shop.Product), ReverseMap = true)]
    public class ProductCreateOrUpdateDto: EntityEntryDto<long?>
    {
        [MapsToAndFromProperty(typeof(Eron.Core.Entities.Financial.Shop.Product), "Name")]
        public string ProductName { get; set; }

        public string ProductCode { get; set; }

        public List<string> Images { get; set; }

        public Guid DefaultImage { get; set; }

        public bool ExistsInShop { get; set; }

        public List<ProductPropertyCreateOrUpdateDto> Properties { get; set; }

        [MapsToAndFromProperty(typeof(Eron.Core.Entities.Financial.Shop.Product), "Price")]
        public long ProductPrice { get; set; }

        [MapsToAndFromProperty(typeof(Eron.Core.Entities.Financial.Shop.Product), "CategoryId")]
        public int ProductCategoryId { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }
    }
}