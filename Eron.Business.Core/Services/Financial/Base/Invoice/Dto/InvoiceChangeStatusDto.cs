using System.Collections.Generic;
using Eron.Core.AppEnums;

namespace Eron.Business.Core.Services.Financial.Base.Invoice.Dto
{
    public class InvoiceChangeStatusDto
    {
        public List<string> Invoices { get; set; }

        public InvoiceStatusType InvoiceStatus { get; set; }

        public string Description { get; set; }
    }
}