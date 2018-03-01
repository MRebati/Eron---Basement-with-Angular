using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Base.Cart.Dto;

namespace Eron.Business.Core.Services.Financial.Base.Cart
{
    public interface ICartAppService: IApplicationService
    {
        Task<List<CartItemDto>> GetUserCartList();

        Task<CartItemDto> Create(CartItemCreateOrUpdateDto input);

        Task<CartItemDto> Update(CartItemCreateOrUpdateDto input);

        Task<bool> Delete(long itemId);
    }
}