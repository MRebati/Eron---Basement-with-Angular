using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Authentication.Dto;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;

namespace Eron.Business.Core.Services.Financial.Order.Order.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Order.Order), ReverseMap = true)]
    public class OrderDto : EntityDto<Guid>
    {
        public OrderDto()
        {
            this.ImageIds = new List<string>();
        }

        public string OrderNumber { get; set; }

        public string Description { get; set; }

        public bool Approved { get; set; }

        public int Count { get; set; }

        public long TariffId { get; set; }

        public long Price { get; set; }

        public long? InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public string UserId { get; set; }

        public OrderStatusType OrderStatus { get; set; }

        public List<string> ImageIds { get; set; }

        public bool HasDesignOrder { get; set; }

        public long DesignPrice { get; set; }

        public long FinalPrice { get; set; }

        public string TariffName { get; set; }

        public long Quantity { get; set; }

        public bool Selected { get; set; }

        public ApplicationUserSummeryDto User { get; set; }
    }
}
