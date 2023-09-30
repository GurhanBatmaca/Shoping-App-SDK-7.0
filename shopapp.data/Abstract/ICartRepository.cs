using shopapp.entity;

namespace shopapp.data.Abstract
{
    public interface ICartRepository : IRepository<Cart>
    {

        Task InitializeCartAsync(string userId);
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task DeleteFromCartAsync(int cartId,int productId);
        Task ClearCartAsync(int cartId);

    }
}