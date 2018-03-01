using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Tariff.Dto;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Financial.Order.Tariff
{
    public interface ITariffAppService : IApplicationService
    {
        #region Query

        Task<PagedListResult<TariffDto>> GetTariffList(IPagedListRequest<Eron.Core.Entities.Financial.Order.Tariff> input);

        Task<List<TariffDto>> GetAll();

        TariffDto Get(long id);

        Task<TariffDto> GetAsync(long id);

        Task<List<TariffDto>> GetByCategoryAsync(int categoryId);

        #endregion

        #region Command

        Task<TariffDto> Create(TariffCreateOrUpdateDto input);

        Task<TariffDto> Update(TariffCreateOrUpdateDto input);

        Task<bool> Delete(long id);

        Task<bool> Delete(TariffDto input);

        #endregion
    }
}