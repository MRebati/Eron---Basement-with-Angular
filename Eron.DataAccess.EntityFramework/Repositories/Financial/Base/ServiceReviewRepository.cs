using System.Data.Entity;
using Eron.Core.Entities.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Base
{
    public class ServiceReviewRepository : Repository<ServiceReview>, IServiceReviewRepository
    {
        public ServiceReviewRepository(DbContext context) : base(context)
        {
        }
    }
}