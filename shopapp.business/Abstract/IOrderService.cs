using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IOrderService: IValidator<Order>
    {
        Task CreateAsync(Order entity);
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetOrdersAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync(string kategori,EnumOrderState orderState,int page,int pageSize);
        Task<Order?> GetByIdWithItemsAsync(int orderId);
        Task UpdateStateAsync(int orderId,EnumOrderState orderState);
    }
}