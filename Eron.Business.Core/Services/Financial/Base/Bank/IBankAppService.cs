using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Core.AppEnums;

namespace Eron.Business.Core.Services.Financial.Base.Bank
{
    public interface IBankAppService : IApplicationService
    {
        Task<string> CreateReference(BankNameType bankName, string userId, long invoiceId);
    }
}