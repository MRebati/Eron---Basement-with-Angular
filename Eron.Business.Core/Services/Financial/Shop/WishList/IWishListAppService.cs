using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.WishList.Dto;
using Eron.Core.Entities.Financial.Base;

namespace Eron.Business.Core.Services.Financial.Shop.WishList
{
    public interface IWishListAppService : IAsyncCrudAppService<long, WishListItem, WishListItemDto, WishListItemCreateDto, WishListItemListDto>
    {
        Task<List<WishListItemDto>> GetUserList(string userId);

        Task<bool> Delete(long id, string userId);

        Task<bool> ProductExistsInUserWishList(long productId, string userId);
    }
}