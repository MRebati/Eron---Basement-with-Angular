using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;

namespace Eron.Business.Core.Services.Financial.Order.TariffCategory
{
    public interface ITariffCategoryAppService: IApplicationService
    {
        #region Query

        Task<List<TariffCategoryDto>> GetAll();

        Task<TariffCategoryDto> GetById(int id);

        Task<List<TariffCategoryDto>> GetTree();

        Task<List<TariffCategoryDto>> GetFullCategories();

        Task<List<TariffCategoryDto>> GetPromoted();

        Task<List<TariffCategoryDto>> GetHomePage();

        #endregion

        #region Command

        Task<TariffCategoryDto> Create(TariffCategoryCreateOrUpdateDto input);

        Task<TariffCategoryDto> Update(TariffCategoryCreateOrUpdateDto input);

        Task<bool> DeleteAsync(int inputId);

        Task<List<TariffCategoryReOrderDto>> ReOrder(List<TariffCategoryReOrderDto> input);

        #endregion
    }
}