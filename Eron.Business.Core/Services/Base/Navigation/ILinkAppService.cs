using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Navigation.Dto;
using Eron.Core.AppEnums;

namespace Eron.Business.Core.Services.Base.Navigation
{
    public interface ILinkAppService: IApplicationService
    {
        Task<List<LinkDto>> GetByPlacement(LinkPlacement placement);

        Task<List<LinkDto>> GetByPlacementAsTree(LinkPlacement placement);

        Task<LinkDto> Create(LinkCreateOrUpdateDto input);

        Task<LinkDto> Update(LinkCreateOrUpdateDto input);

        Task<bool> Delete(int id);

        Task<List<LinkReOrderDto>> ReOrder(List<LinkReOrderDto> input);
    }
}