using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface IOrderService: IValidator<Order>
    {
        Task CreateAsync(Order entity);
        Task<List<Order>> GetInCompleteOrdersAsync(string userId);
    }
}