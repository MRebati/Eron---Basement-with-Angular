using System.Collections.Generic;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Common.Dto;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Common
{
    public interface ICommonAppService : IApplicationService
    {
        #region Query

        List<SelectListObjectDto> GetEnumAsSelectList(string name);

        List<SelectListObjectDto> GetEnumAsSelectListEnglishTitles(string name);

        #endregion
    }
}