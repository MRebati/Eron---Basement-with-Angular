using System;
using Eron.Core.AppEnums;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Order
{
    public class OrderLog: Entity<Guid>
    {
        public string UserId { get; set; }

        public OrderStatusType FromState { get; set; }

        public OrderStatusType ToState { get; set; }

        public string Description { get; set; }
    }
}