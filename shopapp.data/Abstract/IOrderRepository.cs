using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersAsync(string userId,int page,int pageSize);
        Task<int> GetOrdersCountAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync(string category,EnumOrderState orderState,int page,int pageSize);
        Task<int> GetAllOrdersCountAsync(string category,EnumOrderState orderState);
        Task<Order?> GetByIdWithItemsAsync(int orderId);
        Task UpdateStateAsync(int orderId,EnumOrderState orderState);
    }
}