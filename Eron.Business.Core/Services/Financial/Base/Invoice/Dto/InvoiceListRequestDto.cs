using Eron.Core.AppEnums;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Financial.Base.Invoice.Dto
{
    public class InvoiceListRequestDto : PagedListRequest<Eron.Core.Entities.Financial.Base.Invoice>
    {
        public string InvoiceNumber { get; set; }

        public DatePeriodType? DatePeriod { get; set; }

        public InvoiceStatusType? InvoiceStatus { get; set; }

        public InvoiceType? Type { get; set; }

        public bool Paid { get; set; }
    }
}