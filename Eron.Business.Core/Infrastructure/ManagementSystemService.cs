using System.Security.Principal;
using System.Web;
using Eron.Core.AppEnums;
using Eron.DataAccess.Contract.UnitOfWorks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;

namespace Eron.Business.Core.Infrastructure
{
    public class ManagementSystemService : ISystemService
    {
        protected IManagementUnitOfWork UnitOfWork;
        protected TenantType TenantType;

        public ManagementSystemService(IManagementUnitOfWork unitOfWork, TenantType tenantType = TenantType.WebService)
        {
            UnitOfWork = unitOfWork;
        }
    }
}