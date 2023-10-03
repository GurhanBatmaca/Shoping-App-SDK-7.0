using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetOrdersAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync(string kategori,EnumOrderState orderState,int page,int pageSize);
        Task<Order?> GetByIdWithItemsAsync(int orderId);
        Task UpdateStateAsync(int orderId,EnumOrderState orderState);
    }
}