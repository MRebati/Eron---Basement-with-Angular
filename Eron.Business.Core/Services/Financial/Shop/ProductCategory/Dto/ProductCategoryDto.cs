using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;

namespace Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto
{
    [MapsTo(typeof(Eron.Core.Entities.Financial.Shop.ProductCategory))]
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Shop.ProductCategory))]
    public class ProductCategoryDto: EntityDto<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Slug { get; set; }

        public string Keywords { get; set; }

        public int? ParentId { get; set; }

        public int? ProductCounts { get; set; }

        public bool Promoted { get; set; }

        public bool ViewOnHomePage { get; set; }

        public List<ProductCategoryDto> Children { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}
