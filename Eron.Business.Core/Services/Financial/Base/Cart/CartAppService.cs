using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Base.Cart.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Base;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;
using Microsoft.AspNet.Identity;

namespace Eron.Business.Core.Services.Financial.Base.Cart
{
    public class CartAppService: ManagementSystemService, ICartAppService
    {
        public CartAppService(IManagementUnitOfWork unitOfWork, TenantType tenantType = TenantType.WebService) : base(unitOfWork, tenantType)
        {
        }

        public async Task<List<CartItemDto>> GetUserCartList()
        {
            var user = HttpContext.Current.User.Identity.GetUserId();
            var result = await UnitOfWork.CartRepository.GetUserCartList(user);
            return result.MapTo<List<CartItemDto>>();
        }

        public async Task<CartItemDto> Create(CartItemCreateOrUpdateDto input)
        {
            input.UserId = HttpContext.Current.User.Identity.GetUserId();
            var entity = input.MapTo<CartItem>();
            if (entity.Count == 0)
                entity.Count = 1;

            var result = UnitOfWork.CartRepository.Create(entity);
            await UnitOfWork.SaveAsync();
            var product = await UnitOfWork.ProductRepository.GetByIdAsync(input.ProductId);
            var productPrice = await UnitOfWork.ProductPriceRepository.GetActiveForProduct(input.ProductId);

            result.ProductName = product.Name;
            result.ProductPrice = productPrice.Price;
            result.ProductCode = product.ProductCode;
            result.ProductImage = product.DefaultImage.ToString();
            return result.MapTo<CartItemDto>();
        }

        public async Task<CartItemDto> Update(CartItemCreateOrUpdateDto input)
        {
            var entity = input.MapTo<CartItem>();
            UnitOfWork.CartRepository.Update(entity);
            await UnitOfWork.SaveAsync();
            var result = input.MapTo<CartItemDto>();
            var product = await UnitOfWork.ProductRepository.GetByIdAsync(input.ProductId);
            var productPrice = await UnitOfWork.ProductPriceRepository.GetActiveForProduct(input.ProductId);

            result.ProductName = product.Name;
            result.ProductPrice = productPrice.Price;
            result.ProductCode = product.ProductCode;
            result.ProductImage = product.DefaultImage.ToString();
            return result;
        }

        public async Task<bool> Delete(long itemId)
        {
            if (!await UnitOfWork.CartRepository.GetExistsAsync())
                return false;
            UnitOfWork.CartRepository.Delete(itemId);
            await UnitOfWork.SaveAsync();
            return true;
        }
    }
}
