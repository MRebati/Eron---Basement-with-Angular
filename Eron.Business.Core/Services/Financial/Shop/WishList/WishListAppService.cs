using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.WishList.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Base;
using Eron.Core.Exceptions;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Business.Core.Services.Financial.Shop.WishList
{
    public class WishListAppService : AsyncCrudAppService<long, WishListItem, WishListItemDto, WishListItemCreateDto, WishListItemListDto> , IWishListAppService
    {
        private readonly IManagementUnitOfWork _unitOfWork;
        public WishListAppService(
            IRepository<WishListItem> repository, 
            IManagementUnitOfWork managementUnitOfWork) : base(repository)
        {
            _unitOfWork = managementUnitOfWork;
        }

        public async Task<List<WishListItemDto>> GetUserList(string userId)
        {
            var result = await _unitOfWork.WishListRepository.GetAsync(x => x.UserId == userId, x => x.OrderByDescending(y => y.CreateDateTime), "Product");
            return result.MapTo<List<WishListItemDto>>();
        }

        public async Task<bool> Delete(long id, string userId)
        {
            var entity = await _unitOfWork.WishListRepository.GetByIdAsync(id);
            if (entity != null && entity.UserId == userId)
            {
                _unitOfWork.WishListRepository.Delete(entity);
                await _unitOfWork.SaveAsync();
                return true;
            }
            if (entity == null)
                throw new EntityNotFoundException();
            throw new UnauthorizedAccessException();
        }

        public Task<bool> ProductExistsInUserWishList(long productId, string userId)
        {
            var result =
                _unitOfWork.WishListRepository.GetExistsAsync(x => x.ProductId == productId && x.UserId == userId);
            return result;
        }
    }
}
