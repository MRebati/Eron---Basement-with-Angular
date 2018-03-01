using Eron.DataAccess.Contract.Repositories.Base;

namespace Eron.DataAccess.Contract.UnitOfWorks
{
    public interface IDefaultUnitOfWork : IUnitOfWork
    {
        IEronFileRepository FileRepository { get; }
    }
}
