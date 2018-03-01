using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.Entities.Base;
using Eron.Core.Entities.Financial.Shop;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Base
{
    public interface IEronFileRepository: IRepository<EronFile>
    {
        Task<IEnumerable<EronFile>> GetProductImages(long productId);
    }
}
