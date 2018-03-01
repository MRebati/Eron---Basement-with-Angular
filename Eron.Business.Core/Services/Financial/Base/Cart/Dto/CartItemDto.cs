using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;
using Eron.Core.Entities.Financial.Base;

namespace Eron.Business.Core.Services.Financial.Base.Cart.Dto
{
    [MapsFrom(typeof(CartItem), ReverseMap = true)]
    public class CartItemDto: EntityDto<long>
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public long ProductPrice { get; set; }

        public string ProductCode { get; set; }

        public string ProductImage { get; set; }

        public string UserId { get; set; }

        public bool IsSold { get; set; }

        public virtual ProductDto Product { get; set; }
    }

    [MapsFrom(typeof(CartItem), ReverseMap = true)]
    [MapsFrom(typeof(CartItemDto), ReverseMap = true)]
    public class CartItemCreateOrUpdateDto : EntityEntryDto<long>
    {
        public long ProductId { get; set; }

        public int Count { get; set; }

        public string UserId { get; set; }
    }
}
