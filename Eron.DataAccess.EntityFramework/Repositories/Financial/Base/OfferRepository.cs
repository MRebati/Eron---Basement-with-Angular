using System.Data.Entity;
using Eron.Core.Entities.Financial.Base;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Base
{
    public class OfferRepository : Repository<Offer>, IOfferRepository
    {
        public OfferRepository(DbContext context) : base(context)
        {
        }
    }
}