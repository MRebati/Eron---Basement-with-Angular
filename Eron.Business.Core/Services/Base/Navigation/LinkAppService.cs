using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Base.Navigation.Dto;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.Core.Exceptions;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.Mapper;

namespace Eron.Business.Core.Services.Base.Navigation
{
    public class LinkAppService : ManagementSystemService, ILinkAppService
    {
        public LinkAppService(IManagementUnitOfWork unitOfWork, TenantType tenantType = TenantType.WebService) : base(unitOfWork, tenantType)
        {
        }

        #region Query

        public async Task<List<LinkDto>> GetByPlacement(LinkPlacement placement)
        {
            var entityList = await UnitOfWork.LinkRepository.GetByPlcement(placement);
            return entityList.MapTo<List<LinkDto>>();
        }

        public async Task<List<LinkDto>> GetByPlacementAsTree(LinkPlacement placement)
        {
            var entityList = await UnitOfWork.LinkRepository.GetByPlcement(placement);

            var parentEntities = entityList.Where(x => !x.ParentId.HasValue);

            return parentEntities.MapTo<List<LinkDto>>();
        }

        #endregion Query

        public async Task<LinkDto> Create(LinkCreateOrUpdateDto input)
        {
            var createEntity = input.MapTo<Link>();
            UnitOfWork.LinkRepository.Create(createEntity);
            await UnitOfWork.SaveAsync();
            return createEntity.MapTo<LinkDto>();
        }

        public async Task<LinkDto> Update(LinkCreateOrUpdateDto input)
        {
            var updateEntity = input.MapTo<Link>();
            UnitOfWork.LinkRepository.Update(updateEntity);
            await UnitOfWork.SaveAsync();
            return updateEntity.MapTo<LinkDto>();
        }

        public async Task<bool> Delete(int id)
        {
            if (!UnitOfWork.LinkRepository.GetExists(x => x.Id == id))
                throw new EntityNotFoundException();
            UnitOfWork.LinkRepository.Delete(id);
            await UnitOfWork.SaveAsync();
            return true;
        }

        public async Task<List<LinkReOrderDto>> ReOrder(List<LinkReOrderDto> input)
        {
            var entities = (await UnitOfWork.LinkRepository.GetAllAsync()).ToList();

            var parents = entities.Where(x => input.Select(y => y.Id).Any(z => z == x.Id));

            foreach (var item in parents)
            {
                item.ParentId = null;
                AddChildren(item, entities, input);
                UnitOfWork.LinkRepository.Update(item);
            }

            await UnitOfWork.SaveAsync();
            return input;
        }

        #region helpers

        private void AddChildren(Link parent,
            List<Link> collection, List<LinkReOrderDto> childMap)
        {
            var newChildMap = childMap.FirstOrDefault(x => x.Id == parent.Id)?.Children;
            if (newChildMap != null)
                foreach (var item in newChildMap)
                {
                    var childEntity = collection.FirstOrDefault(x => x.Id == item.Id);
                    if (childEntity != null)
                        childEntity.ParentId = parent.Id;
                    if (item.HasChildren)
                        AddChildren(childEntity, collection, childMap);
                }
        }

        #endregion
    }
}
