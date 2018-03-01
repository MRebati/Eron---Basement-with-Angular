using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Pages.Dto;
using Eron.Business.Core.Services.Base.Search.Dto;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;
using Eron.Business.Core.Services.Financial.Order.TariffCategory.Dto;
using Eron.Business.Core.Services.Financial.Shop.Product.Dto;
using Eron.Business.Core.Services.Financial.Shop.ProductCategory.Dto;
using Eron.Core.AppEnums;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Business.Core.Services.Base.Search.SearchInControlPanel
{
    public interface ISearchInControlPanelAppService : IApplicationService
    {
        Task<FullSearchResponseDto> Search(string searchQuery);
    }

    public class SearchInControlPanelAppService: ManagementSystemService, ISearchInControlPanelAppService
    {
        public SearchInControlPanelAppService(IManagementUnitOfWork unitOfWork, TenantType tenantType = TenantType.WebService) : base(unitOfWork, tenantType)
        {
        }


        public async Task<FullSearchResponseDto> Search(string queryString)
        {
            var products =
                await UnitOfWork.ProductRepository.GetAsync(x => x.Name.Contains(queryString) ||
                                                                 x.ProductCode.Contains(queryString));

            var productCategories =
                await UnitOfWork.ProductCategoryRepository.GetAsync(x => x.Title.Contains(queryString) ||
                                                                         x.Slug.Contains(queryString) ||
                                                                         x.Keywords.Contains(queryString));

            var tariffs =
                await UnitOfWork.TariffRepository.GetAsync(x => x.TariffName.Contains(queryString));

            var tariffCategories =
                await UnitOfWork.TariffCategoryRepository.GetAsync(x => x.Title.Contains(queryString) ||
                                                                        x.Slug.Contains(queryString) ||
                                                                        x.Description.Contains(queryString));

            var pages =
                await UnitOfWork.PageRepository.GetAsync(x => x.Title.Contains(queryString) ||
                                                              x.Slug.Contains(queryString) ||
                                                              x.Keywords.Contains(queryString) ||
                                                              x.Description.Contains(queryString));

            var result = new FullSearchResponseDto
            {
                Tariffs = tariffs.MapTo<List<TariffDto>>(),
                TariffCategories = tariffCategories.MapTo<List<TariffCategoryDto>>(),
                Pages = pages.MapTo<List<PageDto>>(),
                Products = products.MapTo<List<ProductDto>>(),
                ProductCategories = productCategories.MapTo<List<ProductCategoryDto>>()
            };

            return result;
        }
    }
}
