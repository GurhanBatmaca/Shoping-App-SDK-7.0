using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetInCompleteOrdersAsync(string userId);
    }
}