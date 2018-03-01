using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.ProductPrice.Dto;

namespace Eron.Business.Core.Services.Financial.Shop.Product.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Shop.Product), ReverseMap = true)]
    public class ProductDto : EntityDto<long>
    {
        public string Name { get; set; }

        public string ProductCode { get; set; }

        public List<string> Images { get; set; }

        public Guid DefaultImage { get; set; }

        public bool ExistsInShop { get; set; }

        public virtual List<ProductPropertyDto> Properties { get; set; }

        public virtual List<ProductPriceWithoutProductDto> Prices { get; set; }

        public long Price { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string ShortDescription { get; set; }

        public string LongDescription { get; set; }
    }
}
