using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.DataAccess.Contract.UnitOfWorks;
using Eron.DataAccess.EntityFramework.UnitOfWorks;
using Ninject.Modules;

namespace Eron.SharedKernel.DependancyResolver.DataAccessModules
{
    public class UnitOfWorkModule: NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUnitOfWork>().To<DefaultUnitOfWork>();
            this.Bind<IManagementUnitOfWork>().To<ManagementUnitOfWork>();
            this.Bind<IDefaultUnitOfWork>().To<DefaultUnitOfWork>();
        }
    }
}
