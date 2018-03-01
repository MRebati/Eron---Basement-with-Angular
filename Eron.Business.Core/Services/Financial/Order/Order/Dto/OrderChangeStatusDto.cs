using System.Collections.Generic;
using Eron.Core.AppEnums;

namespace Eron.Business.Core.Services.Financial.Order.Order.Dto
{
    public class OrderChangeStatusDto
    {
        public List<string> Orders { get; set; }

        public OrderStatusType OrderStatus { get; set; }

        public string Description { get; set; }
    }
}