using shopapp.entity;

namespace shopapp.business.Abstract
{
    public interface ICartService: IValidator<Cart>
    {
        Task InitializeCartAsync(string userId);
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task AddToCartAsync(string userId,int productId,int quantity);
        Task DeleteFromCartAsync(int cartId,int productId);
    }
}