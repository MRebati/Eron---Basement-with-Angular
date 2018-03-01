using System;
using System.Collections.Generic;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Authentication.Dto;
using Eron.Core.AppEnums;
using Eron.Core.ManagementSettings;

namespace Eron.Business.Core.Services.Financial.Base.Invoice.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Base.Invoice), ReverseMap = true)]
    public class InvoiceDto : EntityDto<long>
    {
        public InvoiceDto()
        {
            this.InvoiceItems = new List<InvoiceItemDto>();
        }

        public DateTime ExpireDateTime { get; set; }

        public bool Expired { get; set; }

        public bool Paid { get; set; }

        public long Amount { get; set; }

        public int TotalCount { get; set; }

        public string ReferenceId { get; set; }

        public string InvoiceNumber { get; set; }

        public ICollection<InvoiceItemDto> InvoiceItems { get; set; }

        public ApplicationUserSummeryDto User { get; set; }

        public InvoiceStatusType InvoiceStatus { get; set; }

        public InvoiceType Type { get; set; }

        public int Progress { get; set; }

        public int MaxProgress = ApplicationSettings.FinancialSettings.MaximumInvoiceProgressSteps;
    }
}