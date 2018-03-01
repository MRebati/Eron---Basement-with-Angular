using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Pages.Dto;

namespace Eron.Business.Core.Services.Base.Pages
{
    public interface IPageAppService: IApplicationService
    {
        #region Query

        Task<List<PageDto>> GetAll();

        Task<PageDto> GetDetailsAsync(int id);

        Task<PageDto> GetBySlug(string slug);

        #endregion

        #region Command

        Task<PageDto> Create(PageCreateUpdateDto input);

        Task<PageDto> Update(PageCreateUpdateDto input);

        Task<bool> Delete(EntityDto<int> input);

        #endregion
    }
}