using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.AppEnums;

namespace Eron.Business.Core.Services.Financial.Order.Tariff.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Order.Tariff), ReverseMap = true)]
    public class TariffDto: EntityDto<long>
    {
        public TariffDto()
        {
            this.TariffItems = new List<TariffItemDto>();
        }
        public string TariffName { get; set; }

        public long? TariffPrice { get; set; }

        public long? DesignPrice { get; set; }

        public UnitType UnitType { get; set; }

        public Guid? ImageId { get; set; }

        [MapsToAndFromProperty(typeof(Eron.Core.Entities.Financial.Order.Tariff), "TariffCategoryId")]
        public int CategoryId { get; set; }

        public CustomerType CustomerType { get; set; }

        public List<TariffItemDto> TariffItems { get; set; }
    }
}
