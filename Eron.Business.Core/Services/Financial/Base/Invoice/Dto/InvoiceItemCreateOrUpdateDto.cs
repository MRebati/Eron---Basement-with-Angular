using System;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Financial.Base.Invoice.Dto
{
    public class InvoiceItemCreateOrUpdateDto : EntityEntryDto<long>
    {
        public int Count { get; set; }

        public string Description { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? ProductPriceId { get; set; }

        public Guid? TariffPriceId { get; set; }

        public long InvoiceId { get; set; }
    }
}