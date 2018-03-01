using System.Data.Entity;
using Eron.Core.Entities.Base;
using Eron.DataAccess.Contract.Repositories;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories
{
    public class TenantRepository : Repository<Tenant>, ITenantRepository
    {
        public TenantRepository(DbContext context) : base(context)
        {
        }
    }
}