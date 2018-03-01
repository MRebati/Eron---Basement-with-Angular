using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Financial.Shop.Product
{
    public interface IProductAppService: IApplicationService
    {
        #region Query

        Task<PagedListResult<ProductDto>> GetAllAsPagedList(ProductListRequestDto input);

        Task<List<ProductDto>> GetAll();

        Task<List<ProductDto>> GetByCategoryId(int categoryId);

        Task<List<ProductDto>> GetPromoted();

        Task<ProductDto> GetByProductCode(string productCode);

        Task<ProductCreateOrUpdateDto> GetByProductCodeForUpdate(string productCode);

        ProductDto GetById(long id);

        Task<List<ProductDto>> GetRelatedProductsByProductCode(string productCode);

        Task<List<ProductDto>> GetProductsInCategory(int categoryId);

        Task<List<ProductDto>> GetBestSellers();

        #endregion Query

        #region Command

        Task<ProductDto> Create(ProductCreateOrUpdateDto input);

        Task<ProductDto> Update(ProductCreateOrUpdateDto input);

        Task<bool> Delete(ProductDto input);

        Task<bool> Delete(long id);

        #endregion Command
    }
}