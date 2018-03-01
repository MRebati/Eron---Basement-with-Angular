using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eron.Core.AppEnums;
using Eron.Core.ManagementSettings;
using Eron.Core.ValueObjects;
using Eron.DataAccess.Contract.Infrastructure;

namespace Eron.DataAccess.Contract.Repositories.Financial.Order
{
    public interface IOrderRepository : IRepository<Core.Entities.Financial.Order.Order>
    {
        #region Query

        Task<List<Core.Entities.Financial.Order.Order>> GetOrders(Expression<Func<Core.Entities.Financial.Order.Order, bool>> predicate = null);

        Task<PagedListResult<Core.Entities.Financial.Order.Order>> GetOrdersAsPagedList(
            Expression<Func<Core.Entities.Financial.Order.Order, bool>> predicate,
            string orderNumber,
            int pageNumber = 0,
            int pageSize = ApplicationSettings.Pagination.PageSize,
            OrderStatusType? orderStatus = null,
            DatePeriodType periodType = DatePeriodType.All
        );

        Task<Core.Entities.Financial.Order.Order> GetOrderById(Guid id);

        Task<Core.Entities.Financial.Order.Order> GetOrderByNumberAsync(string orderNumber);

        Task<List<Core.Entities.Financial.Order.Order>> GetOrderListByNumberListAsync(List<string> orderNumberList);

        #endregion Query

        #region Command

        Task<Core.Entities.Financial.Order.Order> ChangeStateOfOrderAsync(string orderNumber,
            OrderStatusType oldOrderStatus, OrderStatusType newOrderStatus, string userId, string description = null);

        Task<Core.Entities.Financial.Order.Order> ChangeStateOfOrderForceAsync(string orderNumber, OrderStatusType newOrderStatus, string userId, string description = null);

        Task<List<Core.Entities.Financial.Order.Order>> ChangeStateOfOrderListAsync(List<string> orderNumber,
            OrderStatusType oldOrderStatus, OrderStatusType newOrderStatus, string userId, string description = null);

        Task<List<Core.Entities.Financial.Order.Order>> ChangeStateOfOrderListForceAsync(List<string> orderNumber, OrderStatusType newOrderStatus, string userId, string description = null);

        #endregion Command
    }
}
