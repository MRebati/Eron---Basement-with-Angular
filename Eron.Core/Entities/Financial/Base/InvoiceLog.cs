using System;
using Eron.Core.AppEnums;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class InvoiceLog: Entity<Guid>
    {
        public string UserId { get; set; }

        public InvoiceStatusType FromState { get; set; }

        public InvoiceStatusType ToState { get; set; }

        public string Description { get; set; }
    }
}