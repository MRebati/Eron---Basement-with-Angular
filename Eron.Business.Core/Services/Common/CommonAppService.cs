using System.Collections.Generic;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Common.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Infrastructure;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Business.Core.Services.Common
{
    public class CommonAppService : ManagementSystemService, ICommonAppService
    {
        public CommonAppService(IManagementUnitOfWork unitOfWork, TenantType tenantType = TenantType.WebService) : base(unitOfWork, tenantType)
        {
        }

        #region Query

        public List<SelectListObjectDto> GetEnumAsSelectList(string name)
        {
            var result = EnumHelper.GetByEnumName(name);
            return result.MapTo<List<SelectListObjectDto>>();
        }

        public List<SelectListObjectDto> GetEnumAsSelectListEnglishTitles(string name)
        {
            var result = EnumHelper.GetByEnumName(name, true);
            return result.MapTo<List<SelectListObjectDto>>();
        }

        #endregion
    }
}