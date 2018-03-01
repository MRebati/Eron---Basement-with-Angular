using Eron.Business.Core.Services.Financial.Order.Tariff;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.SharedKernel.Helpers.FileHelper;

namespace Eron.Presentation.WebApplication.WebApi.Infrastructure
{
    public class BaseManagementController : BaseApiController
    {
        protected IManagementUnitOfWork UnitOfWork;
        protected ITariffAppService TariffService;
        public BaseManagementController(IManagementUnitOfWork unitOfWork,
            ITariffAppService tariffService)
        {
            this.TariffService = tariffService;
            this.UnitOfWork = unitOfWork;
        }
    }
}