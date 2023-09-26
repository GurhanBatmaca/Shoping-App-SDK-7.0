using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {

        Task InitializeCart(string userId);
        // Task<Cart> GetByUserId(string userId);

    }
}