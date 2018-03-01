using System.Threading.Tasks;
using Eron.Core.Entities.Base;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Base
{
    public interface IPageRepository : IRepository<Page>
    {
        Task<Page> GetBySlugAsync(string slug);
    }
}