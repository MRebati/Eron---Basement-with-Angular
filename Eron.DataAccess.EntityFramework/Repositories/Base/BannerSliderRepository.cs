using System.Data.Entity;
using Eron.Core.Entities.Base;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Base
{
    public class BannerSliderRepository : Repository<BannerSlider>, IBannerSliderRepository
    {
        public BannerSliderRepository(DbContext context) : base(context)
        {
        }
    }
}