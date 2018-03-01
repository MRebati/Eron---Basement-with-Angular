using System.Collections.Generic;
using System.ComponentModel.Composition;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Infrastructure
{
    public class ContextConfiguration
    {
        [ImportMany(typeof(IEntityConfiguration))]
        public IEnumerable<IEntityConfiguration> Configurations
        {
            get; set;
        }
    }
}
