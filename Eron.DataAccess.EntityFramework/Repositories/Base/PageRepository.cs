using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.Entities.Base;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Base
{
    public class PageRepository : Repository<Page>, IPageRepository
    {
        public PageRepository(DbContext context) : base(context)
        {
        }

        public async Task<Page> GetBySlugAsync(string slug)
        {
            var result = await (GetQueryable().FirstOrDefaultAsync(x => x.Slug == slug));
            return result;
        }
    }
}