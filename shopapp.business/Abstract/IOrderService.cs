using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IOrderService: IValidator<Order>
    {
        Task CreateAsync(Order entity);
        Task<List<Order>> GetOrdersAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetByIdWithItemsAsync(int orderId);
        Task UpdateStateAsync(int orderId);
    }
}