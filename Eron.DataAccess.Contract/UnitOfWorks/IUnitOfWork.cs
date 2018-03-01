using System;
using System.Threading.Tasks;
using System.Transactions;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {    
        void Save();
        Task SaveAsync();
        Task CompleteAsync();
        TransactionScope Begin();
        void Complete();
    }
}
