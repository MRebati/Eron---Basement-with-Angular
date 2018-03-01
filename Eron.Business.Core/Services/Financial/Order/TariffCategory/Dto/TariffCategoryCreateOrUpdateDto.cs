using System;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto
{
    [MapsTo(typeof(Eron.Core.Entities.Financial.Order.TariffCategory))]
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Order.TariffCategory))]
    [MapsTo(typeof(TariffCategoryDto))]
    [MapsFrom(typeof(TariffCategoryDto))]
    public class TariffCategoryCreateOrUpdateDto : EntityEntryDto<int>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public string Slug { get; set; }

        public Guid? ImageId { get; set; }
    }
}