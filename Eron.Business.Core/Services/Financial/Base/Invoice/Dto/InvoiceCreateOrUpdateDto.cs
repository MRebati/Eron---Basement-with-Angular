using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Financial.Base.Invoice.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Base.Invoice), ReverseMap = true)]
    public class InvoiceCreateOrUpdateDto : EntityEntryDto<long>
    {
        public InvoiceCreateOrUpdateDto()
        {
            this.InvoiceItems = new List<InvoiceItemCreateOrUpdateDto>();
        }

        public DateTime ExpireDateTime { get; set; }

        public bool Expired { get; set; }

        public bool Paid { get; set; }

        public long Amount { get; set; }

        public string ReferenceId { get; set; }

        public string InvoiceNumber { get; set; }

        public List<InvoiceItemCreateOrUpdateDto> InvoiceItems { get; set; }
    }
}
