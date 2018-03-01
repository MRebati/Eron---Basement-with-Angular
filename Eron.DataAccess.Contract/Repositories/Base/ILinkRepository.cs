using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Base
{
    public interface ILinkRepository : IRepository<Link>
    {
        Task<List<Link>> GetByPlcement(LinkPlacement placement);
    }
}