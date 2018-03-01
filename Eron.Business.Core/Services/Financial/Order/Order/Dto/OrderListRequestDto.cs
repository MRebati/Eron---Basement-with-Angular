using Eron.Core.AppEnums;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Financial.Order.Order.Dto
{
    public class OrderListRequestDto : PagedListRequest<Eron.Core.Entities.Financial.Order.Order>
    {
        public DatePeriodType DatePeriod { get; set; }

        public OrderStatusType? OrderStatus { get; set; }

        public string OrderNumber { get; set; }
    }
}