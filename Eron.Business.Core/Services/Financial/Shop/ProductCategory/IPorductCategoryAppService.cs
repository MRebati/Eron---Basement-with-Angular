using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Shop.Product;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;

namespace Eron.Business.Core.Services.Financial.Shop.ProductCategory
{
    public interface IProductCategoryAppService : IApplicationService
    {
        #region Query

        Task<List<ProductCategoryDto>> GetAll();

        Task<ProductCategoryDto> GetById(int id);

        Task<List<ProductCategoryDto>> GetTree();

        Task<List<ProductCategoryDto>> GetFullCategories();

        Task<List<ProductCategoryDto>> GetPromoted();

        Task<List<ProductCategoryDto>> GetHomePage();

        #endregion

        #region Command

        Task<ProductCategoryDto> Create(ProductCategoryCreateOrUpdateDto input);

        Task<ProductCategoryDto> Update(ProductCategoryCreateOrUpdateDto input);

        Task<bool> Delete(int inputId);

        Task<List<ProductCategoryReOrderDto>> ReOrder(List<ProductCategoryReOrderDto> input);

        #endregion

    }
}
