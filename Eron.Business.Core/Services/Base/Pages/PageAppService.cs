using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Pages.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.Core.Exceptions;
using Eron.Core.ManagementSettings;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;
using Microsoft.AspNet.Identity;

namespace Eron.Business.Core.Services.Base.Pages
{
    public class PageAppService : ManagementSystemService, IPageAppService
    {
        public PageAppService(
            IManagementUnitOfWork unitOfWork,
            TenantType tenantType = TenantType.WebService
        ) : base(unitOfWork, tenantType) { }

        #region Query

        public async Task<List<PageDto>> GetAll()
        {
            var results = await UnitOfWork.PageRepository.GetAllAsync();
            return results.MapTo<List<PageDto>>();
        }

        public async Task<PageDto> GetDetailsAsync(int id)
        {
            var result = await UnitOfWork.PageRepository.GetByIdAsync(id);
            return result.MapTo<PageDto>();
        }

        public async Task<PageDto> GetBySlug(string slug)
        {
            var result = await UnitOfWork.PageRepository.GetBySlugAsync(slug);
            return result.MapTo<PageDto>();
        }

        #endregion

        #region Command

        public async Task<PageDto> Create(PageCreateUpdateDto input)
        {
            var model = input.MapTo<Page>();
            model.CreatorUserId = HttpContext.Current.User.Identity.GetUserId();
            model.Language = ApplicationSettings.DefaultLanguage.ToString();
            var pageEntity = UnitOfWork.PageRepository.Create(model);
            await UnitOfWork.SaveAsync();
            return pageEntity.MapTo<PageDto>();
        }

        public async Task<PageDto> Update(PageCreateUpdateDto input)
        {
            if (!input.IsUpdateEntry())
            {
                throw new EntityNotFoundException();
            }
            var model = input.MapTo<Page>();
            UnitOfWork.PageRepository.Update(model);
            await UnitOfWork.SaveAsync();
            return model.MapTo<PageDto>();
        }

        public async Task<bool> Delete(EntityDto<int> input)
        {
            //bug: change this code. it is not an elegent way to check conditions
            try
            {
                UnitOfWork.PageRepository.Delete(input.Id);
                await UnitOfWork.SaveAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
