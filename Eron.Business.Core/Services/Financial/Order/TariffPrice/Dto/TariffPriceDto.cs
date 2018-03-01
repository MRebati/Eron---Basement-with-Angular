using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;

namespace Eron.Business.Core.Services.Financial.Order.TariffPrice.Dto
{
    public class TariffPriceDto: EntityDto<Guid>
    {
        public long Price { get; set; }

        public bool IsValid { get; set; }

        public long TariffId { get; set; }

        public TariffDto Tariff { get; set; }

        public TariffPriceDto Create(long price, long tariffId)
        {
            this.Id = Guid.NewGuid();
            this.IsValid = true;
            this.TariffId = tariffId;
            this.Price = price;
            return this;
        }
    }
}
