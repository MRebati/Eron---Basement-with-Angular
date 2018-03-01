using System;

namespace Eron.Core.Infrastructure
{
    public interface IEntity
    {
        Object Id { get; set; }

        bool IsDeleted { get; set; }
    }
}