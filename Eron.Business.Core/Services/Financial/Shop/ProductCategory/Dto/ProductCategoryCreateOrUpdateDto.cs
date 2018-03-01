using System;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto
{
    [MapsTo(typeof(Eron.Core.Entities.Financial.Shop.ProductCategory))]
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Shop.ProductCategory))]
    public class ProductCategoryCreateOrUpdateDto : EntityEntryDto<int?>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Slug { get; set; }

        public int? ParentId { get; set; }

        public string Keywords { get; set; }

        public bool Promoted { get; set; }

        public bool ViewOnHomePage { get; set; }
    }
}