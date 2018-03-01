using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eron.Core.AppEnums;
using Eron.Core.Entities.Financial.Order;
using Eron.Core.Entities.Financial.Shop;
using Eron.Core.Infrastructure;

namespace Eron.Core.Entities.Financial.Base
{
    public class ServiceReview: Entity<Guid>
    {
        public string Username { get; set; }

        public string Message { get; set; }

        public string Title { get; set; }

        public StarRate StarRate { get; set; }

        public virtual Product Product { get; set; }

        public virtual Tariff Tariff { get; set; }

        public long? ProductId { get; set; }

        public long? TariffId { get; set; }
    }
}
