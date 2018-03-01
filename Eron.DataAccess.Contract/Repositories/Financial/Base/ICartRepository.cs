using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Base;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Base
{
    public interface ICartRepository : IRepository<CartItem>
    {
        Task<List<CartItem>> GetUserCartList(string userId);
        Task<List<CartItem>> GetUserUnpaidCartList(string user);
    }
}