using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Text;
using System.Threading.Tasks;
using Eron.Business.Core.Infrastructure;
using Eron.Business.Core.Services.Financial.Order.Order.Dto;
using Eron.Core.ValueObjects;

namespace Eron.Business.Core.Services.Financial.Order.Order
{
    public interface IOrderAppService : IApplicationService
    {
        #region Query

        Task<List<Dto.OrderDto>> GetUserOrders();

        Task<OrderDto> GetById(Guid id);

        Task<OrderDto> GetByNumber(string orderNumber);

        Task<List<Dto.OrderDto>> GetAllUsersOrders();

        Task<PagedListResult<Dto.OrderDto>> GetAllUsersOrdersAsPagedList(OrderListRequestDto input);

        Task<List<Dto.OrderDto>> GetAllUsersApprovedOrders();

        Task<List<Dto.OrderDto>> GetAllUsersUnpaidOrders();

        Task<List<Dto.OrderDto>> GetDesignPricePendingOrders();

        #endregion Query

        #region Command

        Task<OrderDto> CreateOrder(OrderCreateOrUpdateDto input);

        Task<OrderDto> ApproveOrder(Guid orderId);

        Task<OrderDto> AssignOrderDesignPrice(OrderDesignPriceDto input);

        Task<bool> CancelOrderByOrderNumberAsUser(List<string> orderNumbers);

        Task<bool> ChangeStateOfOrderList(OrderChangeStatusDto input, string userId);

        #endregion Command
    }
}
