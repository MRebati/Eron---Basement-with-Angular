using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Order;

namespace Eron.Business.Core.Services.Financial.Order.Tariff.Dto
{
    [MapsFrom(typeof(TariffItem))]
    [MapsTo(typeof(TariffItem))]
    public class TariffItemDto: EntityDto<long>
    {
        [MapsToAndFromProperty(typeof(TariffItem), "Type")]
        public TariffItemType TariffItemType { get; set; }

        public string Name { get; set; }

        public long TariffId { get; set; }
    }

    [MapsFrom(typeof(TariffItem))]
    [MapsTo(typeof(TariffItem))]
    public class TariffItemCreateOrUpdateDto : EntityDto<long>
    {
        [MapsToAndFromProperty(typeof(TariffItem), "Type")]
        public TariffItemType TariffItemType { get; set; }

        public string Name { get; set; }

        public long? TariffId { get; set; }
    }
}