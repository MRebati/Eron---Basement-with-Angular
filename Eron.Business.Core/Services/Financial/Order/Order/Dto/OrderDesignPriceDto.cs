using System;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;

namespace Eron.Business.Core.Services.Financial.Order.Order.Dto
{
    [MapsFrom(typeof(Eron.Core.Entities.Financial.Order.Order), ReverseMap = true)]
    public class OrderDesignPriceDto : EntityEntryDto<Guid>
    {
        public long DesignPrice { get; set; }
    }
}