using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdWithItemsAsync(int orderId);
        Task UpdateStateAsync(int orderId,EnumOrderState orderState);
    }
}