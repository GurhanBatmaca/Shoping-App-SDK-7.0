using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IOrderService: IValidator<Order>
    {
        Task CreateAsync(Order entity);
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetOrdersAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdWithItemsAsync(int orderId);
        Task UpdateStateAsync(int orderId,EnumOrderState orderState);
    }
}