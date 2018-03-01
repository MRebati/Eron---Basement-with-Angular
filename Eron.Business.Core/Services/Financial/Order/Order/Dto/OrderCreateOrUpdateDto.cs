using System;
using System.Collections.Generic;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;

namespace Eron.Business.Core.Services.Financial.Order.Order.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Order.Order), ReverseMap = true)]
    public class OrderCreateOrUpdateDto: EntityEntryDto<Guid>
    {
        public OrderCreateOrUpdateDto()
        {
            this.ImageIds = new List<string>();
        }

        public string OrderNumber { get; set; }

        public string Description { get; set; }

        public int Count { get; set; }

        public long TariffId { get; set; }

        public TariffDto Tariff { get; set; }

        public bool HasDesignOrder { get; set; }

        public long DesignPrice { get; set; }

        public List<string> ImageIds { get; set; }
    }
}