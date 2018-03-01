using System;
using System.Collections.Generic;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.AppEnums;

namespace Eron.Business.Core.Services.Financial.Order.Tariff.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Order.Tariff), ReverseMap = true)]
    [MapsFrom(typeof(TariffDto), ReverseMap = true)]
    public class TariffCreateOrUpdateDto: EntityEntryDto<long?>
    {
        public TariffCreateOrUpdateDto()
        {
            this.TariffItems = new List<TariffItemCreateOrUpdateDto>();
        }
        public string TariffName { get; set; }

        public long TariffPrice { get; set; }

        public int TariffCategoryId { get; set; }

        public Guid? ImageId { get; set; }

        public long? DesignPrice { get; set; }

        public UnitType UnitType { get; set; }

        public CustomerType CustomerType { get; set; }

        public List<TariffItemCreateOrUpdateDto> TariffItems { get; set; }
    }
}