using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.BannerSlider.Dto;
using Eron.Core.Entities.Base;

namespace Eron.Business.Core.Services.Base.BannerSlider
{
    public interface IBannerSliderAppService: IApplicationService
    {
        Task<BannerSliderDto> GetById(int id);

        Task<List<BannerSliderDto>> GetAllAsync();

        Task<List<BannerSliderDto>> GetByGroupName(string groupName);

        Task<BannerSliderDto> Create(BannerSliderCreateOrUpdateDto input);

        Task<List<BannerSliderDto>> CreateByGroup(List<BannerSliderCreateOrUpdateDto> input);

        Task<BannerSliderDto> Update(BannerSliderCreateOrUpdateDto input);

        Task<List<BannerSliderDto>> UpdateByGroup(List<BannerSliderCreateOrUpdateDto> input);

        Task<bool> Delete(int id);

        Task<bool> DeleteGroup(string groupName);
    }
}
