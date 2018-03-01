using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Eron.Core.Entities.Base;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Base
{
    public class EronFileRepository: Repository<EronFile>, IEronFileRepository
    {
        public EronFileRepository(DbContext context) : base(context)
        {
        }

        public Task<IEnumerable<EronFile>> GetProductImages(long productId)
        {
            return this.GetAsync(x => x.ProductId == productId);
        }
    }
}
