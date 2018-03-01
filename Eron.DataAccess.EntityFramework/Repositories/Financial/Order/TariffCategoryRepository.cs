using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Order;
using Eron.DataAccess.Contract.Infrastructure;
using Eron.DataAccess.Contract.Repositories.Financial.Order;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Financial.Order
{
    public class TariffCategoryRepository : Repository<TariffCategory>, ITariffCategoryRepository
    {
        private readonly IRepository<Tariff> _tariffRepositoy;

        public TariffCategoryRepository(
            DbContext context,
            IRepository<Tariff> tariffRepositoy
            ) : base(context)
        {
            _tariffRepositoy = tariffRepositoy;
        }

        public async Task<List<TariffCategory>> GetFullCategories()
        {
            var result = await (from category in GetQueryable()
                                join tariff in _tariffRepositoy.GetQueryable() on category.Id equals tariff.TariffCategoryId
                                select category).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<TariffCategory>> GetHomePageCategories()
        {
            var result = await(from category in GetQueryable()
                               join tariff in _tariffRepositoy.GetQueryable() on category.Id equals tariff.TariffCategoryId
                               where category.ViewOnHomePage
                select category).Distinct().ToListAsync();
            return result;
        }
    }
}