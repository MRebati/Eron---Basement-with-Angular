using System;
using System.Collections.Generic;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;

namespace Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto
{
    [MapsTo(typeof(Eron.Core.Entities.Financial.Order.TariffCategory))]
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Order.TariffCategory))]
    public class TariffCategoryDto: EntityDto<int>
    {
        public TariffCategoryDto()
        {
            this.Tariffs = new List<TariffDto>();
        }
        public string Title { get; set; }

        public string Description { get; set; }

        public string Keywords { get; set; }

        public string Slug { get; set; }

        public Guid ImageId { get; set; }

        public List<TariffDto> Tariffs { get; set; }
    }
}
