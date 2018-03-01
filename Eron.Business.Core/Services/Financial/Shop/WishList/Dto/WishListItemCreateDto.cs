using AutoMapper.Attributes;
using Eron.Business.Core.Infrastructure;
using Eron.Core.Entities.Financial.Base;

namespace Eron.Business.Core.Services.Financial.Shop.WishList.Dto
{
    [MapsFrom(typeof(WishListItem), ReverseMap = true)]
    public class WishListItemCreateDto: EntityEntryDto<long>
    {
        public string UserId { get; set; }

        public long ProductId { get; set; }
    }
}