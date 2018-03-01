using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.BannerSlider.Dto;
using Eron.Core.AppEnums;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.FileHelper;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Business.Core.Services.Base.BannerSlider
{
    public class BannerSliderAppService: ManagementSystemService, IBannerSliderAppService
    {

        private IFileHelper _fileHelper;

        public BannerSliderAppService(
            IManagementUnitOfWork unitOfWork, 
            IFileHelper fileHelper, 
            TenantType tenantType = TenantType.WebService
        ) : base(unitOfWork, tenantType)
        {
            _fileHelper = fileHelper;
        }

        #region Query

        public async Task<BannerSliderDto> GetById(int id)
        {
            var entity = await UnitOfWork.BannerSliderRepository.GetByIdAsync(id);
            var result = entity.MapTo<BannerSliderDto>();
            return result;
        }

        public async Task<List<BannerSliderDto>> GetAllAsync()
        {
            var entities = await UnitOfWork.BannerSliderRepository.GetAllAsync();
            var result = entities.MapTo<List<BannerSliderDto>>();
            return result;
        }

        public async Task<List<BannerSliderDto>> GetByGroupName(string groupName)
        {
            var entities = await UnitOfWork.BannerSliderRepository.GetAsync(x => x.GroupName == groupName);
            var result = entities.MapTo<List<BannerSliderDto>>();
            return result;
        }

        #endregion Query

        #region Command

        public Task<BannerSliderDto> Create(BannerSliderCreateOrUpdateDto input)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BannerSliderDto>> CreateByGroup(List<BannerSliderCreateOrUpdateDto> input)
        {
            var entityList = input.MapTo<List<Eron.Core.Entities.Base.BannerSlider>>();
            foreach (var inputItem in entityList)
            {
                var fileEntity = await _fileHelper.GetFileAsync(inputItem.FileId);
                await _fileHelper.TransferToDatabaseAsync(fileEntity);
                UnitOfWork.BannerSliderRepository.Create(inputItem);
            }
            await UnitOfWork.SaveAsync();
            return entityList.MapTo<List<BannerSliderDto>>();
        }

        public Task<BannerSliderDto> Update(BannerSliderCreateOrUpdateDto input)
        {
            throw new NotImplementedException();
        }

        public Task<List<BannerSliderDto>> UpdateByGroup(List<BannerSliderCreateOrUpdateDto> input)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Delete(int id)
        {
            UnitOfWork.BannerSliderRepository.Delete(id);
            await UnitOfWork.SaveAsync();
            return true;
        }

        public Task<bool> DeleteGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        #endregion Command
    }
}