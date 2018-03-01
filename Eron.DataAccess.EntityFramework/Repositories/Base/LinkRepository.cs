using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Base;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Base
{
    public class LinkRepository : Repository<Link>, ILinkRepository
    {
        public LinkRepository(DbContext context) : base(context)
        {
        }

        public async Task<List<Link>> GetByPlcement(LinkPlacement placement)
        {
            var result = await GetQueryable().Where(x => x.LinkPlacement == placement).ToListAsync();
            return result;
        }
    }
}