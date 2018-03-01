using System;
using System.Data.Entity;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.DataAccess.EntityFramework.Infrastructure;
using Eron.DataAccess.EntityFramework.Repositories.Base;

namespace Eron.DataAccess.EntityFramework.UnitOfWorks
{
    public class DefaultUnitOfWork : UnitOfWork, IDefaultUnitOfWork
    {
        private IEronFileRepository _fileRepository;

        public DefaultUnitOfWork() : base(new ApplicationDbContext())
        {
        }

        public IEronFileRepository FileRepository
        {
            get
            {
                if (_fileRepository == null)
                    _fileRepository = new EronFileRepository(Context);
                return _fileRepository;
            }
        }
    }
}
