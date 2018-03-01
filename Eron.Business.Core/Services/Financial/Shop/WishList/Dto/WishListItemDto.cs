using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Authentication.Dto;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;
using Eron.Core.Entities.Financial.Base;

namespace Eron.Business.Core.Services.Financial.Shop.WishList.Dto
{
    [MapsFrom(typeof(WishListItem), ReverseMap = true)]
    public class WishListItemDto: EntityDto<long>
    {
        public string UserId { get; set; }

        public bool IsSold { get; set; }

        public long ProductId { get; set; }

        public ProductDto Product { get; set; }
    }
}
