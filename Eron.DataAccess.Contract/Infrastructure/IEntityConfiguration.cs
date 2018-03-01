using System.Data.Entity.ModelConfiguration.Configuration;

namespace Eron.DataAccess.Contract.Infrastructure
{
    public interface IEntityConfiguration
    {
        void AddConfiguration(ConfigurationRegistrar registrar);
    }
}
