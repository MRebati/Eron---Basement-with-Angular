using System.Collections.Generic;
using System.Threading.Tasks;
using Eron.Core.Entities.Financial.Order;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Order
{
    public interface ITariffCategoryRepository : IRepository<TariffCategory>
    {
        Task<List<TariffCategory>> GetFullCategories();

        Task<List<TariffCategory>> GetHomePageCategories();
    }
}