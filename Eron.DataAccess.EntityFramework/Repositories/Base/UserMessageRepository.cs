using System.Data.Entity;
using Eron.Core.Entities.Financial.Base;
using Eron.DataAccess.Contract.Repositories.Base;
using Eron.DataAccess.EntityFramework.Infrastructure;

namespace Eron.DataAccess.EntityFramework.Repositories.Base
{
    public class UserMessageRepository : Repository<UserMessage>, IUserMessageRepository
    {
        public UserMessageRepository(DbContext context) : base(context)
        {
        }
    }
}